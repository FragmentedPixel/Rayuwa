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
    [HideInInspector]public Color pathColor;
    public aPath path;
    private bool followingPath;
    #endregion

    #endregion

    #region Initialization
    private void Start()
    {
        pathColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
	}
    #endregion

    #region Callback Path Found
    public void OnPathFound(List<Node> pathNodes, Vector3[] waypoints, bool pathSuccessful)
    {
		if (pathSuccessful)
        {
			path = new aPath(pathNodes, waypoints, transform.position, turnDst, stoppingDst);

			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}
    #endregion

    #region Following Path
    private IEnumerator FollowPath()
    {
        if (isIdle())
            yield break;

        followingPath = true;
        int pathIndex = 0;
        transform.LookAt(path.lookPoints[0]);

        float speedPercent = 1;

        while (followingPath)
        {
            Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
            while (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
            {
                if (pathIndex == path.finishLineIndex)
                {
                    followingPath = false;
                    break;
                }
                else
                {
                    pathIndex++;
                }
            }

            if (followingPath)
            {

                if (pathIndex >= path.slowDownIndex && stoppingDst > 0)
                {
                    speedPercent = Mathf.Clamp01(path.turnBoundaries[path.finishLineIndex].DistanceFromPoint(pos2D) / stoppingDst);
                    if (speedPercent < 0.01f)
                    {
                        followingPath = false;
                    }
                }

                Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
                transform.Translate(Vector3.forward * Time.deltaTime * speed * speedPercent, Space.Self);
            }

            yield return null;

        }
    }
    #endregion

    #region Gizmos
    public void OnDrawGizmos() {
		if (path != null) {
			path.DrawWithGizmos ();
		}
	}
    private void Update()
    {
        if (path != null)
        {
            Grid grid = FindObjectOfType<Grid>();

            Node currentNode = grid.NodeFromWorldPoint(transform.position);
            List<Node> nodes = grid.GetNeighbours(currentNode);

            foreach (Node n in nodes)
                path.nodes.Remove(n);
        }
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
    }

    public void Resume()
    {
        StopCoroutine("FollowPath");
        StartCoroutine("FollowPath");
    }

    private bool isIdle()
    {
        UnitController controller = GetComponent<UnitController>();
        if (controller == null)
            return false;

        return controller.battleStarted;
    }

    #endregion
}
