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
    public MeshRenderer selectedRenderer;
    [HideInInspector]public Color pathColor;
    private bool followingPath;
    private Grid grid;

    public List<Node> pathNodes;
    private Vector3[] waypoints;
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

			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
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
            while (finalPosition != transform.position)
            {
                if (followingPath == false)
                    yield break;

                transform.position = Vector3.MoveTowards(transform.position, finalPosition, speed * Time.deltaTime);
                yield return null;
            }
        }

        followingPath = false;
    }
    #endregion

    #region Gizmos
    public void DisplaySelected(bool value)
    {
        selectedRenderer.enabled = value;
    }
    #endregion

    #region Agent Methods
    public bool HasReachedDest()
	{
		return !followingPath;
	}
    public void MoveToDestination(Vector3 newDestination)
    {
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
        pathNodes.Clear();
    }
    private bool isIdle()
    {
        UnitController controller = GetComponent<UnitController>();
        if (controller == null)
            return false;

        return !controller.battleStarted;
    }
    #endregion
}