using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Agent))]
public class UnitController : MonoBehaviour
{
    #region Variabiles
    #region States
    public iUnitState currentState;

    public AggroState aggroState;
    public BattleState battleState;
    public FightState fightState;
    public IdleState idleState;
    #endregion

    #region Targeting
    public Transform target;
    public Agent agent;
    #endregion

    #region Parameters
    public bool fightStarted = false;
    public float fightRange = 5f;
    #endregion

    #region Start
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
        agent = GetComponent<Agent>();
    }
    #endregion
    #endregion

    #region CurrentState
    private void Update()
    {
        currentState.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }
    #endregion

    #region Methods
    public void StartFight()
    {
        fightStarted = true;
    }

    public void LookAtTarget()
    {
        Vector3 lookPoint = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.LookAt(lookPoint);
    }
    #endregion

}
