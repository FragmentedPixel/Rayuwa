using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState : iUnitState
{
    #region Constructor
    public BattleState(UnitController controller) : base(controller)
    { }
    #endregion

    #region State Methods
    public override void Update()
    {
        if (controller.agent.target != controller.target)
            controller.agent.target = controller.target;

        controller.LookAtTarget();

        float distance = Vector3.Distance(controller.transform.position, controller.target.position);
        if (distance < controller.fightRange)
            ToAggroState();

    }

    public override void OnTriggerEnter(Collider other)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Methods

    #endregion

    #region Transitions
    public override void ToAggroState()
    {
        controller.currentState = controller.aggroState;
    }

    public override void ToBattleState()
    {
        controller.currentState = controller.battleState;
    }

    public override void ToFighttate()
    {
        controller.currentState = controller.fightState;
    }
    #endregion
}
