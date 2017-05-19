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
    public bool IsDragging;
    private Vector3 MouseDownPoint, CurrentDownPoint;
    #endregion

    #region UnitsLists
    public List<UnitHealth> allUnits = new List<UnitHealth>();
    public List<UnitHealth> unitsInDrag = new List<UnitHealth>();
    public List<UnitHealth> selectedunits = new List<UnitHealth>();
    #endregion

    #region Box
    private float BoxWidth, BoxHeight, BoxLeft, BoxTop;
    private Vector2 BoxStart, BoxFinish;
    public Texture2D boxTexture;
    #endregion

    #region Showing Path
    public GameObject wayPoint;
    public Text unitName;
    public LayerMask walkableMask;
    #endregion
    #endregion

    private void Start()
    {
        allUnits = FindObjectsOfType<UnitHealth>().ToList();
        selectedunits = FindObjectsOfType<UnitHealth>().ToList();
    }

    #region GUI
    private void OnGUI()
    {
        if(boxTexture!= null)
            GUI.skin.box.normal.background = boxTexture;

        if (IsDragging)
            GUI.Box(new Rect(BoxLeft, BoxTop, BoxWidth, BoxHeight), "Cred ca ne trebuie ceva mai frumos aici.");
    }
    #endregion

    #region
    private void Update()
    {
        if(selectedunits.Count > 0)
        {
            if (Input.GetMouseButtonDown(1))
                DrawShortest();

            if (Input.GetMouseButton(1))
                DrawWithMouse();
        }

        DrawCurrentPath();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit))
            return;

        CurrentDownPoint = hit.point;

        if ((Input.GetMouseButton(0)) && (CheckIfMouseIsDragging()))
            IsDragging = true;

        if (Input.GetMouseButtonUp(0))
        {
            PutUnitsFromDragIntoSelectedUnits();
            IsDragging = false;
        }

        //Down Comands
        if (Input.GetMouseButtonDown(0))
            MouseDownPoint = hit.point;

        if (Input.GetMouseButtonDown(0) && selectedunits.Count <= 0 && (hit.transform.tag == "Unit"))
            selectedunits.Add(hit.transform.GetComponent<UnitHealth>());

        if (Input.GetMouseButtonDown(0) && (selectedunits.Count > 0) && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
        {
            selectedunits.Clear();
            if (hit.transform.tag == "Unit")
                selectedunits.Add(hit.transform.GetComponent<UnitHealth>());
        }

        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (hit.transform.tag == "Unit")
                selectedunits.Add(hit.transform.GetComponent<UnitHealth>());
        }

        //End down comands

        if (IsDragging)
            UpdateBox();
    }

    private void UpdateBox()
    {
        BoxWidth = Camera.main.WorldToScreenPoint(MouseDownPoint).x - Camera.main.WorldToScreenPoint(CurrentDownPoint).x;
        BoxHeight = Camera.main.WorldToScreenPoint(MouseDownPoint).y - Camera.main.WorldToScreenPoint(CurrentDownPoint).y;
        BoxLeft = Input.mousePosition.x;
        BoxTop = (Screen.height - Input.mousePosition.y) - BoxHeight;

        if (BoxWidth > 0f && BoxHeight < 0f)
            BoxStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        else if (BoxWidth > 0f && BoxHeight > 0f)
            BoxStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y + BoxHeight);
        else if (BoxWidth < 0f && BoxHeight < 0f)
            BoxStart = new Vector2(Input.mousePosition.x + BoxWidth, Input.mousePosition.y);
        else if (BoxWidth < 0f && BoxHeight > 0f)
            BoxStart = new Vector2(Input.mousePosition.x + BoxWidth, Input.mousePosition.y + BoxHeight);

        BoxFinish = new Vector2(BoxStart.x + Unsigned(BoxWidth), BoxStart.y - Unsigned(BoxHeight));
    }
    #endregion

    #region UnitInDrag Update

    private void LateUpdate()
    {
        unitsInDrag.Clear();

        if ((!IsDragging) || (allUnits.Count <= 0))
            return;

        for (int i = 0; i < allUnits.Count; i++)
        {
            UnitHealth UnitObj = allUnits[i] as UnitHealth;

            if ((!unitsInDrag.Contains(UnitObj)) && (UnitWithinDrag(ScreenPosition(UnitObj.gameObject))))
                unitsInDrag.Add(UnitObj);
        }
    }

    #endregion

    #region Utility

    float Unsigned(float val)
    {
        return (val > 0f) ? (val) : (-val);
    }

    private bool CheckIfMouseIsDragging()
    {
        if (CurrentDownPoint.x - 2 >= MouseDownPoint.x || CurrentDownPoint.y - 2 >= MouseDownPoint.y || CurrentDownPoint.z - 2 >= MouseDownPoint.z ||
            CurrentDownPoint.x < MouseDownPoint.x - 2 || CurrentDownPoint.y < MouseDownPoint.y - 2 || CurrentDownPoint.z < MouseDownPoint.z - 2)
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
        if ((UnitScreenPos.x > BoxStart.x && UnitScreenPos.y < BoxStart.y) && (UnitScreenPos.x < BoxFinish.x && UnitScreenPos.y > BoxFinish.y))
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
        if (unitsInDrag.Count <= 0)
            return;

        selectedunits.Clear();

        for (int i = 0; i < unitsInDrag.Count; i++)
        {
            if (!selectedunits.Contains(unitsInDrag[i]))
                selectedunits.Add(unitsInDrag[i]);
        }

        unitsInDrag.Clear();
    }

    #endregion

    #region Pathing
    private void DrawShortest()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, walkableMask))
            return;

        foreach (UnitHealth unitAgent in selectedunits)
            unitAgent.transform.parent.GetComponent<Agent>().MoveToDestination(hit.point);
    }
    
    private void DrawWithMouse()
    {
        //DrawShortest();
    }

    private void DrawCurrentPath()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        if (selectedunits.Count <= 0)
            return;

        foreach (UnitHealth unitAgent in selectedunits)
        {
            if (unitAgent.transform.parent.GetComponent<Agent>().path == null)
                continue;
            List<Node> pathNodes = unitAgent.transform.parent.GetComponent<Agent>().path.nodes;
            foreach (Node node in pathNodes)
                Instantiate(wayPoint, node.worldPosition, Quaternion.identity, transform);
        }
    }
    #endregion
}
