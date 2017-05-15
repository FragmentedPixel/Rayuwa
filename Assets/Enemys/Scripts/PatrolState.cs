using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : iEnemyState  
{
	private int wayIndex=0;
    
    #region Constructor
    public PatrolState (EnemyController eController):base(eController)
	{   }
    #endregion

    #region State Methods
    public override void Update ()
	{
        Patrol();	
	}


    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            controller.agent.SetNewDestination(other.transform);
            controller.target = other.transform;
            ToChase();
        }
    }
    #endregion

    #region Methods
    private void Patrol()
    {
        if (controller.agent.HasReachedDest())
        {
            wayIndex = (wayIndex + 1) % controller.WayPointParent.childCount;
            controller.agent.SetNewDestination(controller.WayPointParent.GetChild(wayIndex));
        }
    }
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
