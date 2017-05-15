using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Agent))]
public class UnitController : MonoBehaviour
{
    public iUnitState currentState;

    public AggroState aggroState;
    public BattleState battleState;
    public FightState fightState;
    public IdleState idleState;

    public bool fightStarted = false;
    public Agent agent;

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

    private void Update()
    {
        currentState.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }

    #region Methods
    public void StartFight()
    {
        fightStarted = true;
    }
    #endregion

}
