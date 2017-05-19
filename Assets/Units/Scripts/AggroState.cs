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
        controller.debugCube.material.color = Color.yellow;
        if (controller.target != null)
            AggroTarget();
        else
            controller.UpdateTarget();
    }

    public override void OnTriggerEnter(Transform newTarget)
    {

    }
    #endregion

    #region Methods
    private void AggroTarget()
    {
        controller.LookAtTarget();
        
        if (controller.DistanceToTarget() < controller.fightRange)
            ToFightState();
    }
    #endregion
}
