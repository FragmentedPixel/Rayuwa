using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class ChaseState : iEnemyState  
{
    #region Constructor
    public ChaseState (EnemyController eController):base(eController)
	{
	}
    #endregion

    #region State Methods
    public override void Update ()
	{
        controller.anim.SetBool("Walking", true);


        if (controller.target != null)
            ChaseTarget();
        else
            controller.UpdateTarget();
	}

	public override void OnTriggerEnter (Transform other)
	{

	}
    #endregion

    #region Methods
    private void ChaseTarget()
    {
        controller.LookAtTarget();
        controller.agent.MoveToDestination(controller.target.position);

        if (controller.DistanceToTarget() < controller.attackRange)
            ToAttackState();
    }
    #endregion

}
