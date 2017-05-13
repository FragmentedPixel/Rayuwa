using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : iEnemyState  
{
	private int wayIndex=0;

	public Patrol (EnemyController eController):base(eController)
	{
	}

	public override void Update ()
	{
		
		if (controller.agent.HasReachedDest ())
		{
			wayIndex = (wayIndex + 1 )% controller.WayPointParent.childCount;
			controller.agent.SetNewDestination(controller.WayPointParent.GetChild(wayIndex));
		}
	}

	public override void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag ("Player"))
		{
			controller.agent.SetNewDestination (other.transform);
			controller.target = other.transform;
			ToChase ();
		}
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
