using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : iEnemyState  
{
	private int wayIndex=-1;
    
    #region Constructor
    public PatrolState (EnemyController eController):base(eController)
	{   }
    #endregion

    #region State Methods
    public override void Update ()
	{
        Patrol();	
	}


    public override void OnTriggerEnter(Transform other)
    {
        controller.agent.MoveToDestination(other.position);
        controller.target = other;
        ToChaseState();
    }
    #endregion

    #region Methods
    private void Patrol()
    {
        controller.anim.SetBool("Walking", true);

        if (controller.agent.HasReachedDest())
        {
            wayIndex = (wayIndex + 1) % controller.WayPointParent.childCount;
            controller.target = controller.WayPointParent.GetChild(wayIndex);
        }

        controller.agent.MoveToDestination(controller.target.position);

    }
    #endregion

}
