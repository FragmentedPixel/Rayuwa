using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroState : iUnitState
{
    #region Constructor
    public AggroState(UnitController controller) : base(controller)
    { }
    #endregion

    #region State Methods
    public override void Update()
    {
        throw new NotImplementedException();
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
