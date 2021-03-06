﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Pathfinding : MonoBehaviour
{
    #region Initialization
    private Grid grid;
	
	private void Awake()
    {
		grid = GetComponent<Grid>();
	}
    #endregion

    #region FindPath
    public void FindPath(PathRequest request, Action<PathResult> callback)
    {
        List<Node> pathNodes = new List<Node>();
		Vector3[] waypoints = new Vector3[0];
		bool pathSuccess = false;
		
		Node startNode = grid.NodeFromWorldPoint(request.pathStart);
		Node targetNode = grid.NodeFromWorldPoint(request.pathEnd);

        
        if(!targetNode.walkable)
        {
            foreach (Node no in grid.GetNeighbours(targetNode))
                if (no.walkable)
                    targetNode = no;
        }
        
		startNode.parent = startNode;
		
		if (startNode.walkable && targetNode.walkable)
        {
			Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
			HashSet<Node> closedSet = new HashSet<Node>();
			openSet.Add(startNode);
			
			while (openSet.Count > 0)
            {
				Node currentNode = openSet.RemoveFirst();
				closedSet.Add(currentNode);
				
				if (currentNode == targetNode)
                {
					pathSuccess = true;
					break;
				}
				
				foreach (Node neighbour in grid.GetNeighbours(currentNode))
                {
					if (!neighbour.walkable || closedSet.Contains(neighbour))
						continue;
					
					int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour) + neighbour.movementPenalty;
					if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
						neighbour.gCost = newMovementCostToNeighbour;
						neighbour.hCost = GetDistance(neighbour, targetNode);
						neighbour.parent = currentNode;
						
						if (!openSet.Contains(neighbour))
							openSet.Add(neighbour);
						else 
							openSet.UpdateItem(neighbour);
					}
				}
			}
		}

		if (pathSuccess)
        {
            pathNodes = RetraceNodes(startNode, targetNode);
			waypoints = SimplifyPath(pathNodes);
			pathSuccess = waypoints.Length > 0;
		}

		callback (new PathResult (pathNodes, waypoints, pathSuccess, request.callback));
		
	}
    #endregion

    #region Retracing Nodes
    private List<Node> RetraceNodes(Node startNode, Node endNode)
    {
        List<Node> pathNodes = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            pathNodes.Add(currentNode);
            currentNode = currentNode.parent;
        }
        pathNodes.Reverse();
        return pathNodes;
    }
	private Vector3[] SimplifyPath(List<Node> path)
    {
		List<Vector3> waypoints = new List<Vector3>();
		Vector2 directionOld = Vector2.zero;
		
		for (int i = 1; i < path.Count; i ++)
        {
			Vector2 directionNew = new Vector2(path[i-1].gridX - path[i].gridX,path[i-1].gridY - path[i].gridY);
			if (directionNew != directionOld)
				waypoints.Add(path[i].worldPosition);
			
			directionOld = directionNew;
		}
		return waypoints.ToArray();
	}
    #endregion
    
    #region Utility
    private int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        else
            return 14 * dstX + 10 * (dstY - dstX);
    }
    #endregion
}
