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
        controller.debugCube.material.color = Color.magenta;

        if (controller.target != null)
            controller.agent.MoveToDestination(controller.target.position);
    }

    public override void OnTriggerEnter(Transform newTarget)
    {
        controller.CheckNewTarget(newTarget);
    }
    #endregion

}
