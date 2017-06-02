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
    #endregion

    #region Parameters
    [HideInInspector] public bool battleStarted = false;

    [Header("Attack")]
    public float fightRange = 3f;
    public float fightSpeed = 1f;
	public float fightDmg = 10f;
    #endregion

    #region Amunation
    [HideInInspector] public int ammo;
    public int maxAmmo;
    public float reloadTime = 3f;
    private bool reloading;
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
        ammo = maxAmmo;
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<Agent>();

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
        agent.MoveToDestination(destination);

        target = null;

        if (battleStarted)
            currentState.ToBattleState();
    }

    public void SetNewTarget(Transform newTarget)
    {
        reloading = false;
        target = newTarget;

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
        ammo = maxAmmo;
        UnitHealth health = GetComponentInChildren<UnitHealth>();
        health.Heal();

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
