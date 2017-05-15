using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightState : iUnitState
{
	private float lastAttack = Time.time;

    #region Constructor
    public FightState(UnitController controller) : base(controller)
    { }
    #endregion

    #region State Methods
    public override void Update()
    {

        controller.debugCube.material.color = Color.red;

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
		if (Vector3.Distance (controller.target.transform.position, controller.transform.position) > controller.fightRange)
			ToAggroState ();
		else if(lastAttack+controller.fightCooldown<Time.time)
		{
			lastAttack = Time.time;
            HitTarget();
		}
    }

    public void HitTarget()
    {
		controller.target.GetComponent<EnemyHealth>().Hit(controller.fightDamage);
    }
    #endregion
}
