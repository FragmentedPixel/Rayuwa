using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightState : iUnitState
{
	public float lastAttack = Time.time;

    #region Constructor
    public FightState(UnitController controller) : base(controller)
    { }
    #endregion

    #region State Methods
    public override void Update()
    {
        controller.anim.SetBool("Walking", false);

        if (controller.target != null)
            FightTarget();
        else
            controller.CheckForNearbyEnemies();
    }

    public override void HitByEnemy(Transform newTarget)
    {
        //Controller already is attacking a target.
    }
    #endregion

    #region Methods
    private void FightTarget()
    {
        controller.LookAtTarget();
        controller.agent.Stop();
        controller.agent.ClearPath();

        if (controller.DistanceToTarget() > controller.fightRange)
			ToAggroState ();
		else if(lastAttack + controller.fightSpeed<Time.time)
            HitTarget();
    }

    public void HitTarget()
    {
		if (controller.ammo <= 0) 
		{
			ToReloadState();
			return;
		}		

        controller.ammo--;

        lastAttack = Time.time;

        controller.FightTarget();

		if (controller.ammo <= 0)
			ToReloadState();
    }
    #endregion
}
