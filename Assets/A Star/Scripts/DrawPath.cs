using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPath : MonoBehaviour
{
    public GameObject gridCube;

    Grid grid;

    private void Start()
    {
        grid = FindObjectOfType<Grid>();
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
            DrawShortest();
        if (Input.GetMouseButton(1))
            DrawWithMouse();
    }

    private Node finalNode;

    private void DrawShortest()
    {
        foreach (Transform t in transform)
            Destroy(t.gameObject);
        Vector3 finalPosition = transform.position;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            finalPosition = hit.point;

        finalNode = grid.NodeFromWorldPoint(finalPosition);
        PathRequestManager.RequestPath(new PathRequest(transform.position, finalPosition, OnPathFound));
    }

    private void OnPathFound(Vector3[] arg1, bool arg2)
    {
            Debug.Log("da");
            List<Node> path = new List<Node>();
            Node currentNode = finalNode;
            Node startNode = grid.NodeFromWorldPoint(transform.position);

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }

            foreach(Node n in path)
            {
                Instantiate(gridCube, n.worldPosition, Quaternion.identity, transform);
            }
    }

    private void DrawWithMouse()
    {
        Vector3 mouseWorld = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            mouseWorld = hit.point;
        
        Node currentNode = grid.NodeFromWorldPoint(mouseWorld);
        if (currentNode.walkable)
            Instantiate(gridCube, currentNode.worldPosition, Quaternion.identity, transform);
    }

}
