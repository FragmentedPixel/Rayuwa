﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class iUnitState
{
    public UnitController controller;

    #region Constructor
    public iUnitState(UnitController uController)
    {
        controller = uController;
    }
    #endregion

    #region Methods
    public abstract void Update();
    public abstract void HitByEnemy(Transform enemy);
    #endregion

    #region Transition
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

    public void ToReloadState()
    {
        controller.currentState = controller.reloadState;
    }
    #endregion
}
