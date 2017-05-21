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

        //controller.anim.SetBool("Walking", false);

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
        controller.LookAtTarget();
        controller.agent.Stop();

        if (controller.DistanceToTarget() > controller.fightRange)
			ToAggroState ();
		else if(lastAttack + controller.fightSpeed<Time.time)
            HitTarget();
    }

    public void HitTarget()
    {
        lastAttack = Time.time;
        //controller.anim.SetTrigger("Attack");
        controller.target.GetComponent<EnemyHealth>().Hit(controller.fightDmg);
    }
    #endregion
}
