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
        controller.debugCube.material.color = Color.white;

        Idle();
    }

    public override void OnTriggerEnter(Transform newTarget)
    {
        controller.CheckNewTarget(newTarget);
    }
    #endregion

    #region Methods
    private void Idle()
    {
        controller.agent.Stop();

        if (controller.battleStarted)
            ToBattleState();
    }
    #endregion
}

