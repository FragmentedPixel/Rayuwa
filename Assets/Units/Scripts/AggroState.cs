﻿using System;
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
        controller.anim.SetBool("Walking", true);

        if (controller.ammo <= 0)
            ToReloadState();
        else if (controller.target != null)
            AggroTarget();
        else
            controller.CheckForNearbyEnemies();
    }

    public override void HitByEnemy(Transform enemy)
    {}
    #endregion

    #region Methods
    private void AggroTarget()
    {
        controller.destination = controller.target.position;
        controller.agent.MoveToDestination(controller.target.position);

        if (controller.DistanceToTarget() < controller.fightRange)
            ToFightState();
    }
    #endregion
}
