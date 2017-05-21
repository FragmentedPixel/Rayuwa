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
        controller.transform.GetChild(0).GetComponent<Animator>().SetBool("Walking", true);


        if (Vector3.Distance (controller.target.transform.position, controller.transform.position) < controller.attackRange)
			ToAttackState ();

	}

	public override void OnTriggerEnter (Collider other)
	{

	}
    #endregion

    #region Methods

    #endregion

}
