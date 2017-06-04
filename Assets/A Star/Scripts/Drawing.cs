﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Drawing : MonoBehaviour
{   
    #region Variabiles
    #region Dragging
    public bool isdragging;
    public float dragThreshhold = 10f;
    public Texture boxOutline;

    private Vector2 mouseDownPoint, mouseCurrentPoint;
    private RaycastHit hit;
    #endregion

    #region UnitsLists
    public List<Agent> allAgents = new List<Agent>();
    public List<Agent> agentsInDrag = new List<Agent>();
    public List<Agent> selectedAgents = new List<Agent>();
    #endregion

    #region Box
    private float boxWidth, boxHeight, boxLeft, boxTop;
    private Vector2 boxStart, boxFinish;
    #endregion

    #region Showing Path
    public GameObject wayPointPrefab;
    private Grid grid;
    private List<Node> lastSelected;
    #endregion
    #endregion

    #region Initialization
    private void Start()
    {
        grid = FindObjectOfType<Grid>();

        foreach (Node node in grid.grid)
            node.Deactivate();
    }
    #endregion

    #region GUI
    private void OnGUI()
    {
        if (Unsigned(boxWidth) < dragThreshhold)
            isdragging = false;

        if (isdragging)
            GUI.DrawTexture(new Rect(boxLeft, boxTop, boxWidth, boxHeight), boxOutline);
    }
    #endregion

    #region Updates
    private void FixedUpdate()
    {
        DrawSelectedPaths();
        UpdateDragging();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out hit))
            return;

        UpdateSelecting();
        UpdatePathing();
    }
    private void LateUpdate()
    {
        agentsInDrag.Clear();

        if ((!isdragging) || (allAgents.Count <= 0))
            return;

        for (int i = 0; i < allAgents.Count; i++)
        {
            Agent UnitObj = allAgents[i] as Agent;

            if ((!agentsInDrag.Contains(UnitObj)) && (UnitWithinDrag(ScreenPosition(UnitObj.gameObject))))
                agentsInDrag.Add(UnitObj);
        }
    }

    private void UpdateDragging()
    {
        mouseCurrentPoint = Input.mousePosition;

        if ((Input.GetMouseButton(0)) && (CheckIfMouseIsDragging()))
            isdragging = true;

        if (Input.GetMouseButtonUp(0))
        {
            PutUnitsFromDragIntoSelectedUnits();
            isdragging = false;
        }

        if (Input.GetMouseButtonDown(0))
            mouseDownPoint = mouseCurrentPoint;

        if (isdragging)
            UpdateBox();

    }
    private void UpdateBox()
    {
        boxWidth = mouseDownPoint.x - mouseCurrentPoint.x;
        boxHeight = mouseDownPoint.y - mouseCurrentPoint.y;
        boxLeft = Input.mousePosition.x;
        boxTop = (Screen.height - Input.mousePosition.y) - boxHeight;

        if (boxWidth > 0f && boxHeight < 0f)
            boxStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        else if (boxWidth > 0f && boxHeight > 0f)
            boxStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y + boxHeight);
        else if (boxWidth < 0f && boxHeight < 0f)
            boxStart = new Vector2(Input.mousePosition.x + boxWidth, Input.mousePosition.y);
        else if (boxWidth < 0f && boxHeight > 0f)
            boxStart = new Vector2(Input.mousePosition.x + boxWidth, Input.mousePosition.y + boxHeight);

        boxFinish = new Vector2(boxStart.x + Unsigned(boxWidth), boxStart.y - Unsigned(boxHeight));
    }
    private void UpdateSelecting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Agent hitAgent = GetAgentFromTransform(hit.transform);

            if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
                selectedAgents.Clear();

            if (hitAgent != null)
                selectedAgents.Add(hitAgent);
        }
    }
    private void UpdatePathing()
    {
        if (selectedAgents.Count <= 0)
            return;

        if (Input.GetMouseButtonDown(1))
            DrawShortest();
    }
    #endregion

    #region Utility
    private void UnselectUnits()
    {
        for(int i = 0; i < allAgents.Count; i++)
        {
            Agent agent = allAgents[i];

            if(agent == null)
            {
                allAgents.Remove(agent);
                if (selectedAgents.Contains(agent))
                    selectedAgents.Remove(agent);
                if (agentsInDrag.Contains(agent))
                    agentsInDrag.Remove(agent);
            }
            else
                agent.DisplaySelected(false);
        }

        if (lastSelected == null)
            return;

        foreach (Node node in lastSelected)
            node.Deactivate();
    }
    private Agent GetAgentFromTransform(Transform t)
    {
        UnitHealth unitHealth = t.GetComponent<UnitHealth>();
        if (unitHealth == null)
            return null;
        else
            return unitHealth.transform.parent.GetComponent<Agent>();
    }
    private float Unsigned(float val)
    {
        return (val > 0f) ? (val) : (-val);
    }
    private bool CheckIfMouseIsDragging()
    {
        if (mouseCurrentPoint.x - dragThreshhold >= mouseDownPoint.x || mouseCurrentPoint.y - dragThreshhold >= mouseDownPoint.y  ||
            mouseCurrentPoint.x < mouseDownPoint.x - dragThreshhold || mouseCurrentPoint.y < mouseDownPoint.y - dragThreshhold)
            return true;
        else
            return false;
    }
    private bool UnitWithinScreenSpace(Vector2 UnitScreenPos)
    {
        if ((UnitScreenPos.x < Screen.width && UnitScreenPos.y < Screen.height) && (UnitScreenPos.x > 0f && UnitScreenPos.y > 0f))
            return true;
        else
            return false;
    }
    private bool UnitWithinDrag(Vector2 UnitScreenPos)
    {
        if ((UnitScreenPos.x > boxStart.x && UnitScreenPos.y < boxStart.y) && (UnitScreenPos.x < boxFinish.x && UnitScreenPos.y > boxFinish.y))
            return true;
        else
            return false;
    }
    private Vector2 ScreenPosition(GameObject obj)
    {
        return Camera.main.WorldToScreenPoint(obj.transform.position);
    }
    private void PutUnitsFromDragIntoSelectedUnits()
    {
        if (agentsInDrag.Count <= 0)
            return;

        selectedAgents.Clear();

        for (int i = 0; i < agentsInDrag.Count; i++)
        {
            if (!selectedAgents.Contains(agentsInDrag[i]))
                selectedAgents.Add(agentsInDrag[i]);
        }

        agentsInDrag.Clear();
    }
    #endregion

    #region Pathing
    private void DrawShortest()
    {
        EnemyHealth targetHealth = hit.transform.GetComponent<EnemyHealth>();
        if(targetHealth)
        {
            foreach (Agent unitAgent in selectedAgents)
                unitAgent.GetComponent<UnitController>().SetNewTarget(targetHealth.transform);

            return;
        }

        ReloadPoint reloadPoint = hit.transform.GetComponent<ReloadPoint>();
        if (reloadPoint)
        {
            foreach (Agent unitAgent in selectedAgents)
                unitAgent.GetComponent<UnitController>().SetNewReloadPoint(reloadPoint);

            return;
        }
            
        foreach (Agent unitAgent in selectedAgents)
            unitAgent.GetComponent<UnitController>().SetNewDestination(hit.point);
    }
    private void DrawSelectedPaths()
    {
        UnselectUnits();

        lastSelected = new List<Node>();

        for(int i = 0; i < selectedAgents.Count; i++)
        {
            selectedAgents[i].DisplaySelected(true);

            if (selectedAgents[i].pathNodes == null)
                continue;

            List<Node> pathNodes = selectedAgents[i].pathNodes;

            foreach(Node n in pathNodes)
                lastSelected.Add(n);

            for (int j = 0; j < pathNodes.Count - 1; j++)
                pathNodes[j].Activate(selectedAgents[i].pathColor, pathNodes[j + 1].worldPosition);
        }
    }
    #endregion
}
