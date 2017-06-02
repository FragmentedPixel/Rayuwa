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
        controller.anim.SetBool("Walking", true);

        if (controller.target != null)
            AggroTarget();
        else
            ToBattleState();
    }

    public override void HitByEnemy(Transform enemy)
    {}
    #endregion

    #region Methods
    private void AggroTarget()
    {
        controller.LookAtTarget();
        controller.agent.MoveToDestination(controller.target.position);

        if (controller.DistanceToTarget() < controller.fightRange)
            ToFightState();
    }
    #endregion
}
