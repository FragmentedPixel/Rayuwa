using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class iEnemyState  
{
	public EnemyController controller;

    #region Constructor
    public iEnemyState (EnemyController eController)
	{
		controller = eController;
	}
    #endregion

    #region Methods
    public abstract void Update ();
	public abstract void OnTriggerEnter (Collider other);
    #endregion

    #region Transition
    public void ToAttackState()
    {
        controller.currentState = controller.attackState;
    }
    public void ToPatrolState()
    {
        controller.currentState = controller.patrolState;
    }
    public void ToChaseState()
    {
        controller.currentState = controller.chaseState;
    }
    #endregion
}
