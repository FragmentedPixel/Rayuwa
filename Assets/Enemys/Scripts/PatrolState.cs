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
        if (controller.Ammo())
            Patrol();
        else
            RefillAmmo();
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

        if (controller.agent.HasReachedDest() || controller.target ==  null)
        {
            wayIndex = (wayIndex + 1) % controller.WayPointParent.childCount;
            controller.target = controller.WayPointParent.GetChild(wayIndex);
        }

        controller.agent.MoveToDestination(controller.target.position);
    }

    public void RefillAmmo()
    {
        
        controller.anim.SetBool("Walking", true);
        controller.target = controller.refillPlace;
        controller.agent.MoveToDestination(controller.target.position);
        if (Vector3.Distance(controller.transform.position,controller.target.transform.position)<2)
        {
            controller.currentAmmo = controller.maxAmmo;
            controller.target = null;
            controller.UpdateTarget();
            return;
        }
    }

    #endregion

}
