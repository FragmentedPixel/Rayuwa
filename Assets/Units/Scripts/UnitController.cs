using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Agent))]
public abstract class UnitController : MonoBehaviour
{
    public MeshRenderer debugCube;

    #region Variabiles

    #region Targeting + Components
    [HideInInspector] public Animator anim;
    [HideInInspector] public Agent agent;
    [HideInInspector] public Transform target;
    [HideInInspector] public Vector3 destination;
    #endregion

    #region Parameters
    [HideInInspector] public bool battleStarted = false;

    [Header("Attack")]
    public float fightRange = 3f;
    public float fightSpeed = 1f;
	public float fightDmg = 10f;
    #endregion

    #region States
    public iUnitState currentState;

    public AggroState aggroState;
    public BattleState battleState;
    public FightState fightState;
    public IdleState idleState;
    #endregion

    #endregion

    #region Initialization
    private void Awake()
    {
        aggroState = new AggroState(this);
        battleState = new BattleState(this);
        fightState = new FightState(this);
        idleState = new IdleState(this);

        currentState = idleState;
    }
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<Agent>();
        agent.MoveToDestination(GameObject.Find("Crytsal").transform.position);
    }
    #endregion

    #region CurrentState
    private void Update()
    {
        currentState.Update();
    }
    public void HitByEnemy(Transform attacker)
    {
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
        float distance = Vector3.Distance(transform.position, target.position);
        return distance;
    }

    public void SetNewDestination(Vector3 newDestionation)
    {
        destination = newDestionation;
        agent.MoveToDestination(destination);

        target = null;

        if (battleStarted)
            currentState.ToBattleState();
    }

    public void SetNewTarget(Transform newTarget)
    {
        target = newTarget;

        if (!battleStarted)
            return;

        if (DistanceToTarget() < fightRange)
            currentState.ToFightState();
        else
            currentState.ToAggroState();
    }
    #endregion

    public abstract void FightTarget();
}
