using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class iUnitState
{
    public UnitController controller;

    public iUnitState(UnitController controller)
    {
        this.controller = controller;
    }

    public abstract void Update();
    public abstract void OnTriggerEnter(Transform newTarget);

    #region Transition
    public void ToIdleState()
    {
        controller.currentState = controller.idleState;
    }
    public void ToBattleState()
    {
        controller.currentState = controller.battleState;
    }
    public void ToAggroState()
    {
        controller.currentState = controller.aggroState;
    }
    public void ToFightState()
    {
        controller.currentState = controller.fightState;
    }
    #endregion
}
