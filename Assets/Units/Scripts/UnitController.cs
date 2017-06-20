using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Agent))]
public abstract class UnitController : MonoBehaviour
{
    #region Variabiles

    #region Components
    [HideInInspector] public Animator anim;
    [HideInInspector] public Agent agent;
    [HideInInspector] public AudioSource audioS;
    [HideInInspector] public List<EnemyHealth> nearbyEnemies = new List<EnemyHealth>();
    #endregion

    #region Targeting
    [HideInInspector] public Transform target;
    [HideInInspector] public Transform targetToAttack;
    [HideInInspector] public Vector3 destination;
    [HideInInspector] public ReloadPoint reloadPoint;
    #endregion

    #region UI
    [Header("UnitType")]
    public Sprite image;
    public Color color;
    #endregion

    #region Stats
    [Header("Attack stats")]
    public float fightRange = 3f;
    public float fightSpeed = 1f;
	public float fightDmg = 10f;
    public AudioClip fightSound;

    [Header("UpgradeEffects")]
    public int speedIndex;
    public int damageIndex;
    public int healthIndex;
    public int ammoIndex;
    [Range(0,1)]
    public float speedPercent = 0.1f;
    [Range(0, 1)]
    public float damagePercent = 0.05f;
    [Range(0, 1)]
    public float healthPercent = 0.1f;
    public int ammoToAdd = 1;
    #endregion

    #region Amunation
    [HideInInspector] public int ammo;
    [Header("Ammo")]
    public int maxAmmo;
    public float reloadTime = 3f;
    public bool reloading;
    private ParticleSystem ammoParticules;
    #endregion

    #region States
    public iUnitState currentState;

    public AggroState aggroState;
    public BattleState battleState;
    public FightState fightState;
    public ReloadState reloadState;
    #endregion

    #endregion

    #region Initialization
    private void Awake()
    {
        aggroState = new AggroState(this);
        battleState = new BattleState(this);
        fightState = new FightState(this);
        reloadState = new ReloadState(this);

        currentState = battleState;
    }
    private void Start()
    {
        UnitHealth unitHealth;
        
        unitHealth = GetComponentInChildren<UnitHealth>();
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<Agent>();
        ammoParticules = unitHealth.GetComponentInChildren<ParticleSystem>();
        ammoParticules.Stop();

        audioS = GetComponent<AudioSource>();
        audioS.volume = PlayerPrefsManager.GetMasterVolume();

        agent.speed += agent.speed * speedPercent * UpgradesManager.instance.GetUpgradeValue(speedIndex);
        fightDmg += fightDmg * damagePercent * UpgradesManager.instance.GetUpgradeValue(damageIndex);
        unitHealth.MaxHealth += unitHealth.MaxHealth * healthPercent * UpgradesManager.instance.GetUpgradeValue(healthIndex);
        maxAmmo += ammoToAdd * UpgradesManager.instance.GetUpgradeValue(ammoIndex);
        ammo = maxAmmo;

        SetNewDestination(transform.position);
    }
    #endregion

    #region CurrentState
    private void Update()
    {
        currentState.Update();
    }
    public void HitByEnemy(Transform attacker)
    {
        if (attacker == null)
            return;

        EnemyHealth enemy = attacker.GetComponentInChildren<EnemyHealth>();

        if (enemy != null)
            currentState.HitByEnemy(enemy.transform);
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
    #endregion

    #region Methods
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
    #endregion

    #region Targeting
    public void SetNewDestination(Vector3 newDestionation)
    {
        reloading = false;
        destination = newDestionation;

        agent.MoveToDestination(destination);

        reloadPoint = null;
        target = null;
        
        currentState.ToBattleState();
    }
    public void SetNewTarget(Transform newTarget)
    {
        reloading = false;
        target = newTarget;
        
        if (DistanceToTarget() < fightRange)
            currentState.ToFightState();
        else
            currentState.ToAggroState();
    }
    public void SetNewReloadPoint(ReloadPoint point)
    {
        reloadPoint = point;
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
                SetNewTarget(nearbyEnemies[0].transform);
                return;
            }
        }
            
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
        ammoParticules.Play();
        reloading = true;

        float duration = reloadTime;
        float currentTime = 0f;
        while(reloading && currentTime < duration)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }

        if (reloading)
        {
            UnitHealth health = GetComponentInChildren<UnitHealth>();

            health.Heal(1);
            ammo = maxAmmo;
        }

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

        ammoParticules.Stop();
    }
    public abstract string GetAmmoText();
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * fightRange);
    }
    public abstract void FightTarget();
}
