﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Line
{
    #region Line Paramters
    private const float verticalLineGradient = 1e5f;

    private float gradient;
    private float y_intercept;
    private Vector2 pointOnLine_1;
    private Vector2 pointOnLine_2;
    #endregion

    #region Line Variabiles
    private float gradientPerpendicular;
    private bool approachSide;
    #endregion

    #region Constructor
    public Line(Vector2 pointOnLine, Vector2 pointPerpendicularToLine)
    {
		float dx = pointOnLine.x - pointPerpendicularToLine.x;
		float dy = pointOnLine.y - pointPerpendicularToLine.y;

		if (dx == 0)
			gradientPerpendicular = verticalLineGradient;
		else
			gradientPerpendicular = dy / dx;
		

		if (gradientPerpendicular == 0)
			gradient = verticalLineGradient;
		else
			gradient = -1 / gradientPerpendicular;
		

		y_intercept = pointOnLine.y - gradient * pointOnLine.x;
		pointOnLine_1 = pointOnLine;
		pointOnLine_2 = pointOnLine + new Vector2 (1, gradient);

		approachSide = false;
		approachSide = GetSide (pointPerpendicularToLine);
	}
    #endregion

    #region Methods
    private bool GetSide(Vector2 p)
    {
		return (p.x - pointOnLine_1.x) * (pointOnLine_2.y - pointOnLine_1.y) > (p.y - pointOnLine_1.y) * (pointOnLine_2.x - pointOnLine_1.x);
	}
	public bool HasCrossedLine(Vector2 p)
    {
		return GetSide (p) != approachSide;
	}
	public float DistanceFromPoint(Vector2 p)
    {
		float yInterceptPerpendicular = p.y - gradientPerpendicular * p.x;
		float intersectX = (yInterceptPerpendicular - y_intercept) / (gradient - gradientPerpendicular);
		float intersectY = gradient * intersectX + y_intercept;
		return Vector2.Distance (p, new Vector2 (intersectX, intersectY));
	}
    #endregion

    #region Gizmos
    public void DrawWithGizmos(float length)
    {
		Vector3 lineDir = new Vector3 (1, 0, gradient).normalized;
		Vector3 lineCentre = new Vector3 (pointOnLine_1.x, 0, pointOnLine_1.y) + Vector3.up;
		Gizmos.DrawLine (lineCentre - lineDir * length / 2f, lineCentre + lineDir * length / 2f);
	}
    #endregion

}
