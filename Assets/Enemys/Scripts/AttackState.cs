using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class AttackState : iEnemyState  
{
	private float lastAttack = Time.time;

    #region Constructor
    public AttackState (EnemyController eController):base(eController)
	{	}
    #endregion

    #region State Methods
    public override void Update ()
	{
        controller.anim.SetBool("Walking", false);

        if (controller.target != null)
            AttackTarget();
        else
            controller.UpdateTarget();
	}
	public override void OnTriggerEnter (Transform other)
	{
		
	}
    #endregion

    #region Methods

    private void AttackTarget()
    {
        controller.LookAtTarget();
        controller.agent.Stop();

        if (controller.DistanceToTarget() > controller.attackRange)
            ToChaseState();
        else if (lastAttack + controller.attackSpeed < Time.time)
            HitTarget();
    }

    public void HitTarget()
	{
        lastAttack = Time.time;
        controller.AttackTarget(); 
	}
    #endregion

}
