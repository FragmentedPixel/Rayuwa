using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Agent))]
public abstract class UnitController : MonoBehaviour
{
    #region Variabiles

    #region Targeting + Components
    [HideInInspector] public Animator anim;
    [HideInInspector] public Agent agent;
    [HideInInspector] public Transform target;
    [HideInInspector] public Vector3 destination;
    [HideInInspector] public ReloadPoint reloadPoint;
    [HideInInspector] public AudioSource audioS;
    [HideInInspector] public List<EnemyHealth> nearbyEnemies = new List<EnemyHealth>();
    #endregion

    #region Parameters
    [HideInInspector] public bool battleStarted = false;
    [HideInInspector] public bool playerDecided = false;

    [Header("Attack")]
    public float fightRange = 3f;
    public float fightSpeed = 1f;
	public float fightDmg = 10f;
    public AudioClip fightSound;

    [Header("UpgradeEffects")]
    public int speedIndex;
    public int damageIndex;
    public int healthIndex;
    [Range(0,1)]
    public float speedPercent = 0.1f;
    [Range(0, 1)]
    public float damagePercent = 0.05f;
    [Range(0, 1)]
    public float healthPercent = 0.1f;
    #endregion

    #region Amunation
    [HideInInspector] public int ammo;
    public int maxAmmo;
    public float reloadTime = 3f;
    public bool reloading;
    #endregion

    #region States
    public iUnitState currentState;

    public AggroState aggroState;
    public BattleState battleState;
    public FightState fightState;
    public IdleState idleState;
    public ReloadState reloadState;
    #endregion

    #endregion

    #region Initialization
    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward * fightRange);
    }

    private void Awake()
    {
        aggroState = new AggroState(this);
        battleState = new BattleState(this);
        fightState = new FightState(this);
        idleState = new IdleState(this);
        reloadState = new ReloadState(this);

        currentState = idleState;
    }
    private void Start()
    {
        UnitHealth unitHealth;

        ammo = maxAmmo;
        unitHealth = GetComponentInChildren<UnitHealth>();
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<Agent>();

        audioS = GetComponent<AudioSource>();
        audioS.volume = PlayerPrefsManager.GetMasterVolume();

        agent.speed += agent.speed * speedPercent * UpgradesManager.instance.GetUpgradeValue(speedIndex);
        fightDmg += fightDmg * damagePercent * UpgradesManager.instance.GetUpgradeValue(damageIndex);
        unitHealth.MaxHealth += unitHealth.MaxHealth * healthPercent * UpgradesManager.instance.GetUpgradeValue(healthIndex);

        SetNewDestination(transform.position);
    }

    private void AddUpgrades()
    {
        if (UpgradesManager.instance == null)
            return;

        fightDmg += UpgradesManager.instance.GetUpgradeValue(1);
    }
    #endregion

    #region CurrentState
    private void Update()
    {
        currentState.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();

        if (enemy != null && !nearbyEnemies.Contains(enemy))
            nearbyEnemies.Add(enemy);
    }

    private void OnTriggerExit(Collider other)
    {
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();

        if (enemy != null && nearbyEnemies.Contains(enemy))
            nearbyEnemies.Remove(enemy);
    }

    public void HitByEnemy(Transform attacker)
    {
        if (attacker == null)
            return;

        EnemyHealth enemy = attacker.GetComponentInChildren<EnemyHealth>();

        if(enemy != null)
            currentState.HitByEnemy(enemy.transform);
    }
    #endregion

    #region Methods
    public void StartBattle()
    {
        battleStarted = true;
    }
    public void LookAtTarget()
    {
        Vector3 lookPoint = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.LookAt(lookPoint);
    }
    public float DistanceToTarget()
    {
        Vector3 playerPos = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 targetPos = new Vector3(target.position.x, 0f, target.position.z);

        float distance = Vector3.Distance(playerPos, targetPos);
        return distance;
    }
    public void SetNewDestination(Vector3 newDestionation)
    {
        reloading = false;
        destination = newDestionation;
        reloadPoint = null;
        agent.MoveToDestination(destination);

        target = null;

        if (battleStarted)
            currentState.ToBattleState();
    }
    public void SetNewTarget(Transform newTarget, bool _playerDecided)
    {
        reloading = false;
        target = newTarget;

        playerDecided = _playerDecided;

        if (!battleStarted)
            return;

        if (DistanceToTarget() < fightRange)
            currentState.ToFightState();
        else
            currentState.ToAggroState();
    }
    public void SetNewReloadPoint(ReloadPoint point)
    {
        reloadPoint = point;

        if (!battleStarted)
            return;

        currentState.ToReloadState();
    }
    public void CheckForNearbyEnemies()
    {
        //TODO: Find closest
        for(int i = 0; i < nearbyEnemies.Count; i++)
        {
            if(nearbyEnemies[i] == null)
            {
                nearbyEnemies.Remove(nearbyEnemies[i]);
                i--;
            }
            else
            {
                SetNewTarget(nearbyEnemies[0].transform, playerDecided);
                return;
            }
        }
        
        if (playerDecided)
            currentState.ToBattleState();
        else
            SetNewDestination(transform.position);
    }
    #endregion

    #region Resources
    public void Reload()
    {
        if (!reloading)
            StartCoroutine(ReloadingCR());
    }
    private IEnumerator ReloadingCR()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadTime);

        UnitHealth health = GetComponentInChildren<UnitHealth>();
        
        health.Heal(1);
        ammo = maxAmmo;

        reloading = false;

        if (target != null)
        {
            if (DistanceToTarget() < fightRange)
                currentState.ToFightState();
            else
                currentState.ToAggroState();
        }
        else
            currentState.ToBattleState();
    }
    public abstract string GetAmmoText();
    #endregion

    public abstract void FightTarget();
}
