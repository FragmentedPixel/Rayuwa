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
        controller.transform.GetChild(0).GetComponent<Animator>().SetBool("Walking", false);


        if (controller.target == null)
		{
			ToPatrolState ();
			return;
		}
		if (Vector3.Distance (controller.target.transform.position, controller.transform.position) > controller.attackRange)
			ToChaseState ();
		else if(lastAttack+controller.attackSpeed<Time.time)
		{
			lastAttack = Time.time;
			HitTarget ();
		}
	}
	public override void OnTriggerEnter (Collider other)
	{
		
	}
    #endregion

    #region Methods
	public void HitTarget()
	{
        controller.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Attack");
		controller.target.GetComponent<UnitHealth> ().Hit (controller.attackDmg);
	}
    #endregion

}
