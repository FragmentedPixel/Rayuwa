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

    public override void HitByEnemy(Transform newTarget)
    {
        //Fight not started yet.
    }
    #endregion

    #region Methods
    private void Idle()
    {
        if (controller.target != null)
            controller.agent.MoveToDestination(controller.target.position);
        if (controller.reloadPoint)
            controller.agent.MoveToDestination(controller.reloadPoint.transform.position);

        controller.agent.Stop();

        if (controller.battleStarted)
        {
            controller.agent.Resume();

            if (controller.target != null)
                ToAggroState();
            else if (controller.reloadPoint != null)
                ToReloadState();
            else
                ToBattleState();
        }
    }
    #endregion
}

