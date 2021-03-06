﻿using System;
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
        controller.target = null;
        controller.anim.SetBool("Walking", !controller.reloading);

        if (controller.reloadPoint == null)
            FindClosestReloadPoint();

        if (DistanceToReloadPoint() < 3f && !controller.reloading)
            controller.Reload();

        controller.destination = controller.reloadPoint.transform.position;
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

        controller.SetNewReloadPoint(controller.reloadPoint);
    }
    private float DistanceToReloadPoint()
    {
        if (controller.reloadPoint == null)
            return 0f;

        Vector3 playerPos = controller.transform.position;
        Vector3 targetPos = controller.reloadPoint.transform.position;

        playerPos.y = targetPos.y = 0f;

        float distance = Vector3.Distance(playerPos, targetPos);
        return distance;
    }
    #endregion
}
