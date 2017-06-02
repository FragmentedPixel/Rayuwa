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
        bool walking = !controller.agent.HasReachedDest();

        controller.anim.SetBool("Walking", walking);
        controller.agent.MoveToDestination(controller.destination);
    }

    public override void HitByEnemy(Transform enemy)
    {
        controller.SetNewTarget(enemy);
    }
    #endregion
}
