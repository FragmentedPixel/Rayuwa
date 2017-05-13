using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class iEnemyState  
{
	public EnemyController controller;

	public iEnemyState (EnemyController eController)
	{
		controller = eController;
	}

	public abstract void Update ();

	public abstract void OnTriggerEnter (Collider other);

	public abstract void ToAttack ();
	public abstract void ToChase ();
	public abstract void ToPatrol ();

}
