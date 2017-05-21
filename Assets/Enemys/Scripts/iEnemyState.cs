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

    #region Transitions
    public abstract void ToAttack();
	public abstract void ToChase ();
	public abstract void ToPatrol ();
    #endregion
}
