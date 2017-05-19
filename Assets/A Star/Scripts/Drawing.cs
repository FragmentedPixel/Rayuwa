using System;
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
    private Vector2 mouseDownPoint, mouseCurrentPoint;
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
    public LayerMask walkableMask;
    private Grid grid;
    #endregion

    #endregion

    #region Initialization
    private void Start()
    {
        grid = FindObjectOfType<Grid>();

        allAgents = FindObjectsOfType<Agent>().ToList();

        for (int i = 0; i < allAgents.Count; i++)
            if (allAgents[i].GetComponent<EnemyController>())
            {
                allAgents.Remove(allAgents[i]);
                i--;
            }

        selectedAgents = new List<Agent>();
        foreach (Agent a in allAgents)
            selectedAgents.Add(a);
    }
    #endregion

    #region GUI
    private void OnGUI()
    {
        if (Unsigned(boxWidth) < dragThreshhold)
            return;

        if (isdragging)
            GUI.Box(new Rect(boxLeft, boxTop, boxWidth, boxHeight),"Eok");
    }
    #endregion

    #region Updates
    private void Update()
    {
        CheckForUnitsUpdates();
        DrawSelectedPaths();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (selectedAgents.Count > 0)
        {
            if (Input.GetMouseButtonDown(1))
                DrawShortest();

            if (Input.GetMouseButton(1))
                DrawWithMouse();
        }

        if (!Physics.Raycast(ray, out hit))
            return;

        mouseCurrentPoint = Input.mousePosition;

        if ((Input.GetMouseButton(0)) && (CheckIfMouseIsDragging()))
            isdragging = true;

        if (Input.GetMouseButtonUp(0))
        {
            PutUnitsFromDragIntoSelectedUnits();
            isdragging = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            mouseDownPoint = mouseCurrentPoint;
            Agent hitAgent = GetAgentFromTransform(hit.transform);

            if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
                selectedAgents.Clear();

            if (hitAgent != null)
                selectedAgents.Add(hitAgent);
        }

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

    #endregion

    #region Utility
    private void CheckForUnitsUpdates()
    {
        for(int i = 0; i < allAgents.Count; i++)
        {
            Agent agent = allAgents[i];

            if(agent == null)
            {
                allAgents.Remove(agent);
                if (selectedAgents.Contains(agent))
                    selectedAgents.Remove(agent);
            }
        }
    }

    private Agent GetAgentFromTransform(Transform t)
    {
        UnitHealth unitHealth = t.GetComponent<UnitHealth>();
        if (unitHealth == null)
            return null;
        else
            return unitHealth.transform.parent.GetComponent<Agent>();
    }

    float Unsigned(float val)
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

    public bool UnitWithinScreenSpace(Vector2 UnitScreenPos)
    {
        if ((UnitScreenPos.x < Screen.width && UnitScreenPos.y < Screen.height) && (UnitScreenPos.x > 0f && UnitScreenPos.y > 0f))
            return true;
        else
            return false;
    }

    public bool UnitWithinDrag(Vector2 UnitScreenPos)
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

    public void PutUnitsFromDragIntoSelectedUnits()
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit))
            return;

        EnemyHealth targetHealth = hit.transform.GetComponent<EnemyHealth>();
        if(targetHealth)
        {
            foreach (Agent unitAgent in selectedAgents)
                unitAgent.GetComponent<UnitController>().CheckNewTarget(targetHealth.transform);
        }
        else
        {
            foreach (Agent unitAgent in selectedAgents)
                unitAgent.MoveToDestination(hit.point);
        }

        
    }
    
    private void DrawWithMouse()
    {
        //TODO: PLS ADD BUT LATER
    }

    private void DrawSelectedPaths()
    {
        foreach (Node node in grid.grid)
            node.Deactivate();

        if (selectedAgents.Count <= 0)
            return;

        for(int i = 0; i < selectedAgents.Count; i++)
        {
            
            if (selectedAgents[i].GetComponent<Agent>().path == null)
                continue;

            List<Node> pathNodes = selectedAgents[i].GetComponent<Agent>().path.nodes;
            foreach (Node node in pathNodes)
                node.Activate(selectedAgents[i].pathColor);
        }
    }
    #endregion
}
