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
        Debug.Log("Make sure to follow current path");
    }

    public override void OnTriggerEnter(Transform newTarget)
    {
        controller.CheckNewTarget(newTarget);
    }
    #endregion

}
