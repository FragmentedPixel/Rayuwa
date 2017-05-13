using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class AttackState : iEnemyState  
{
    #region Constructor
    public AttackState (EnemyController eController):base(eController)
	{	}
    #endregion

    #region State Methods
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
    #endregion

    #region Methods

    #endregion

    #region Transitions
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
    #endregion
}
