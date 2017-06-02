using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadState : iUnitState
{
    #region Constructor
    public ReloadState(UnitController uController) : base(uController)
    {}
    #endregion

    #region State Methods
    public override void Update()
    {
        controller.anim.SetBool("Walking", true);

        if (controller.reloadPoint == null)
            FindClosestReloadPoint();

        controller.agent.MoveToDestination(controller.reloadPoint.transform.position);
    }

    public override void HitByEnemy(Transform enemy)
    {
        //TODO: Move faster
    }
    #endregion

    #region Methods
    private void FindClosestReloadPoint()
    {
        ReloadPoint[] points = GameObject.FindObjectsOfType<ReloadPoint>();
        float distanceToPoint = int.MaxValue;

        foreach(ReloadPoint point in points)
        {
            float newDistanceToPoint = Vector3.Distance(point.transform.position, controller.transform.position);
            if(newDistanceToPoint < distanceToPoint)
            {
                distanceToPoint = newDistanceToPoint;
                controller.reloadPoint = point;
            }
        }

    }
    #endregion
}
