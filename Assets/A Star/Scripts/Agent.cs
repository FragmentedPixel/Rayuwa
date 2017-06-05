using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Agent : MonoBehaviour
{
    #region Variabiles

    #region Walking Paramters
    [HideInInspector] public Vector3 destination;
	public float speed = 20;
	public float turnSpeed = 3;
	public float turnDst = 5;
	public float stoppingDst = 10;
    #endregion

    #region Path + Following
    public float pathUpdateThreshhold = .5f;
    public MeshRenderer selectedRenderer;
    [HideInInspector]public Color pathColor;
    private bool followingPath;
    private Grid grid;

    public List<Node> pathNodes;
    private Vector3[] waypoints;

    private Node currentNode;
    private Node oldNode;
    #endregion

    #endregion

    #region Initialization
    private void Awake()
    {
        pathColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

        grid = FindObjectOfType<Grid>();
        destination = transform.position;

        if (selectedRenderer != null)
            selectedRenderer.material.color = pathColor;
    }
    #endregion

    #region Following Path
    public void OnPathFound(List<Node> pathNodes, Vector3[] waypoints, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            this.pathNodes = pathNodes;
            this.waypoints = waypoints;
            try
            {
                StopCoroutine("FollowPath");
                StartCoroutine("FollowPath");
            }
            catch { }
        }
	}
    private IEnumerator FollowPath()
    {
        if (isIdle())
            yield break;

        followingPath = true;
       
        for (int i = 0; i < pathNodes.Count; i++)
        {
            Vector3 finalPosition = pathNodes[i].worldPosition;
            Quaternion finalRotation = Quaternion.LookRotation(finalPosition - transform.position);
            while (finalPosition != transform.position)
            {
                if (followingPath == false)
                    yield break;

                transform.position = Vector3.MoveTowards(transform.position, finalPosition, speed * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, finalRotation, Time.deltaTime * turnSpeed);
                yield return null;
            }

            pathNodes.Remove(pathNodes[i]);
            i--;
        }

        followingPath = false;
    }
    #endregion

    #region Gizmos
    public void DisplaySelected(bool value)
    {
        if (selectedRenderer == null)
            return;

        selectedRenderer.enabled = value;
    }

    private void Update()
    {
        PreventColliding();
    }

    private void PreventColliding()
    {
        if (pathNodes != null)
        {
            foreach (Node n in pathNodes)
                if (n.walkable == false)
                {
                    RecalculateDestination();
                    break;
                }
        }

        currentNode = grid.NodeFromWorldPoint(transform.position);

        currentNode.walkable = false;

        if (oldNode != null && oldNode != currentNode)
            oldNode.walkable = true;

        oldNode = currentNode;
    }
    #endregion

    #region Agent Methods
    public bool HasReachedDest()
	{
		return !followingPath;
	}
    public void MoveToDestination(Vector3 newDestination)
    {
        float differnece = Vector3.Distance(newDestination, destination);
        if (differnece < pathUpdateThreshhold)
            return;

        currentNode = grid.NodeFromWorldPoint(transform.position);
        currentNode.walkable = true;

        destination = newDestination;
        PathRequestManager.RequestPath(new PathRequest(transform.position, destination, OnPathFound));
    }
    public void Stop()
    {
        followingPath = false;
        StopCoroutine("FollowPath");
    }
    public void Resume()
    {
        StopCoroutine("FollowPath");
        if(pathNodes != null)
            StartCoroutine("FollowPath");
    }
    public void ClearPath()
    {
        if(pathNodes != null)
        pathNodes.Clear();
    }
    private bool isIdle()
    {
        UnitController controller = GetComponent<UnitController>();
        if (controller == null)
            return false;

        return !controller.battleStarted;
    }
    public void RecalculateDestination()
    {
        currentNode = grid.NodeFromWorldPoint(transform.position);
        currentNode.walkable = true;

        PathRequestManager.RequestPath(new PathRequest(transform.position, destination, OnPathFound));
    }
    #endregion
}