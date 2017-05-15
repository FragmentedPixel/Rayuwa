using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : iUnitState
{
    #region Constructor
    public IdleState(UnitController controller) : base(controller)
    {}
    #endregion

    #region State Methods
    public override void Update()
    {
        if (controller.fightStarted)
            ToBattleState();
    }

    public override void OnTriggerEnter(Collider other)
    {

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
