using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Agent : MonoBehaviour
{
    #region Variabiles
    #region Updates Paramteres
    private const float minPathUpdateTime = .2f;
	private const float pathUpdateMoveThreshold = .5f;
    #endregion

    #region Walking Paramters
    public Transform destination;
	public float speed = 20;
	public float turnSpeed = 3;
	public float turnDst = 5;
	public float stoppingDst = 10;
    #endregion

    #region Path + Following
    public aPath path;
    private bool followingPath;
    #endregion
    #endregion

    #region Initialization
    private void Start()
    {
		StartCoroutine (UpdatePath ());
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

    #region Updating path on target Movement
    private IEnumerator UpdatePath()
    {

		if (Time.timeSinceLevelLoad < .3f)
        {
			yield return new WaitForSeconds (.3f);
		}

		PathRequestManager.RequestPath (new PathRequest(transform.position, destination.position, OnPathFound));

		float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
		Vector3 targetPosOld = destination.position;

		while (true)
        {
			yield return new WaitForSeconds (minPathUpdateTime);

            //TODO: CHECK IF DESTINATION != NULL E NECESAR
            if (destination != null && ((destination.position - targetPosOld).sqrMagnitude > sqrMoveThreshold))
            {
				PathRequestManager.RequestPath (new PathRequest(transform.position, destination.position, OnPathFound));
				targetPosOld = destination.position;
			}
		}
	}
    #endregion

    #region Following Path
    private IEnumerator FollowPath()
    {

		followingPath = true;
		int pathIndex = 0;
		transform.LookAt (path.lookPoints [0]);

		float speedPercent = 1;

		while (followingPath) {
			Vector2 pos2D = new Vector2 (transform.position.x, transform.position.z);
			while (path.turnBoundaries [pathIndex].HasCrossedLine (pos2D)) {
				if (pathIndex == path.finishLineIndex) {
					followingPath = false;
					break;
				} else {
					pathIndex++;
				}
			}

			if (followingPath) {

				if (pathIndex >= path.slowDownIndex && stoppingDst > 0) {
					speedPercent = Mathf.Clamp01 (path.turnBoundaries [path.finishLineIndex].DistanceFromPoint (pos2D) / stoppingDst);
					if (speedPercent < 0.01f) {
						followingPath = false;
					}
				}

				Quaternion targetRotation = Quaternion.LookRotation (path.lookPoints [pathIndex] - transform.position);
				transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
				transform.Translate (Vector3.forward * Time.deltaTime * speed * speedPercent, Space.Self);
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
    #endregion

    #region Agent Methods
    public bool HasReachedDest()
	{
		return !followingPath;
	}

	public void SetNewDestination(Transform destTransform)
	{
		destination = destTransform;
		PathRequestManager.RequestPath (new PathRequest(transform.position, destination.position, OnPathFound));
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

    public void MoveToDestination(Vector3 finalPosition)
    {
        PathRequestManager.RequestPath(new PathRequest(transform.position, finalPosition, OnPathFound));
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
    #endregion

}
