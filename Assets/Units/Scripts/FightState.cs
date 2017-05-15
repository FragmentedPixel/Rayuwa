using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightState : iUnitState
{
    private float currentTime;

    #region Constructor
    public FightState(UnitController controller) : base(controller)
    { }
    #endregion

    #region State Methods
    public override void Update()
    {
        if (controller.target != null)
            FightTarget();
        else
            controller.UpdateTarget();
    }

    public override void OnTriggerEnter(Transform newTarget)
    {
    }
    #endregion

    #region Methods
    private void FightTarget()
    {
        if (currentTime > controller.fightCooldown)
            currentTime += Time.deltaTime;
        else
            HitTarget();
    }

    public void HitTarget()
    {
        Debug.Log("Attack pls");
    }
    #endregion
}
