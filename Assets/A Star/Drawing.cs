using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drawing : MonoBehaviour
{
    public GameObject wayPoint;
    public Text unitName;
    public LayerMask walkableMask;

    private Agent unitAgent;

    private void Update()
    {
        if (unitAgent != null)
            DrawCurrentPath();

        if (Input.GetMouseButtonDown(0))
            SelectUnit();

        if (unitAgent != null && Input.GetMouseButtonDown(1))
            DrawShortest();

        if (unitAgent != null && Input.GetMouseButton(1))
            DrawWithMouse();
    }

    private void SelectUnit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Agent newAgent = hit.transform.GetComponent<Agent>();
            if (newAgent != null)
            {
                unitAgent = newAgent;
                unitName.text = unitAgent.gameObject.name;
            }
        }
    }

    private void DrawShortest()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, walkableMask))
            unitAgent.MoveToDestination(hit.point);
    }
    
    private void DrawWithMouse()
    {
        //DrawShortest();
    }

    private void DrawCurrentPath()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        List<Node> pathNodes = unitAgent.path.nodes;
        foreach (Node node in pathNodes)
            Instantiate(wayPoint, node.worldPosition, Quaternion.identity, transform);
    }
}
