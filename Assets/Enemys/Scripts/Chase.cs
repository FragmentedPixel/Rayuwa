using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Chase : iEnemyState  
{
	public Chase (EnemyController eController):base(eController)
	{
	}

	public override void Update ()
	{
		if (Vector3.Distance (controller.target.transform.position, controller.transform.position) < controller.attackDistance)
			ToAttack ();

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
