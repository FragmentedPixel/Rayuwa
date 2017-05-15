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
        if (controller.battleStarted)
            ToBattleState();
    }

    public override void OnTriggerEnter(Transform newTarget)
    {
        controller.CheckNewTarget(newTarget);
    }
    #endregion
}
