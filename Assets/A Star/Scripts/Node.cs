using UnityEngine;
using System.Collections;

public class Node : IHeapItem<Node>
{
    #region Walkable parameters   
    public bool walkable;
	public Vector3 worldPosition;
	public int gridX;
	public int gridY;
	public int movementPenalty;
    public MeshRenderer pointRenderer;
    #endregion

    #region Costs + index
    public int gCost;
	public int hCost;
	public Node parent;
	int heapIndex;
    #endregion

    #region Constructor
    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY, int _penalty, GameObject _wayPoint)
    {
		walkable = _walkable;
		worldPosition = _worldPos;
		gridX = _gridX;
		gridY = _gridY;
		movementPenalty = _penalty;

        pointRenderer = _wayPoint.GetComponentInChildren<MeshRenderer>();
	}
    #endregion

    #region Properties
    public int fCost {	get {return gCost + hCost;	}	}
	public int HeapIndex {
                            get { return heapIndex;	}
                            set { heapIndex = value;}
                         }
	public int CompareTo(Node nodeToCompare) {
		int compare = fCost.CompareTo(nodeToCompare.fCost);
		if (compare == 0) {
			compare = hCost.CompareTo(nodeToCompare.hCost);
		}
		return -compare;
	}
    #endregion

    #region Methods
    public void Activate(Color color, Vector3 nextPos)
    {
        pointRenderer.enabled = true;
        pointRenderer.material.color = color;

        pointRenderer.transform.parent.LookAt(nextPos);
    }

    public void Deactivate()
    {
        pointRenderer.enabled = false;
    }
    #endregion
}
