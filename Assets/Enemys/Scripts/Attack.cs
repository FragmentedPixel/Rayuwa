using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Attack : iEnemyState  
{
	public Attack (EnemyController eController):base(eController)
	{
	}

	public override void Update ()
	{
		if (controller.target == null)
			ToPatrol ();
		if (Vector3.Distance (controller.target.transform.position, controller.transform.position) > controller.attackDistance)
			ToChase ();
	}
	public override void OnTriggerEnter (Collider other)
	{
		
	}

	public override void ToAttack ()
	{
		controller.currentState = controller.attack;
	}
	public override void ToChase ()
	{
		controller.currentState = controller.chase;	
	}
	public override void ToPatrol ()
	{
		controller.currentState = controller.patrol;	
	}

}
