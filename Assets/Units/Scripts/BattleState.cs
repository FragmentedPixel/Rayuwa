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
        controller.anim.SetBool("Walking", true);

        controller.agent.MoveToDestination(controller.destination);
    }

    public override void HitByEnemy(Transform enemy)
    {
        controller.SetNewTarget(enemy);
    }
    #endregion
}
