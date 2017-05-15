using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;

public class PathRequestManager : MonoBehaviour
{
    #region Variabiles
    private static PathRequestManager instance;

    private Queue<PathResult> results = new Queue<PathResult>();
	private Pathfinding pathfinding;
    #endregion

    #region Initialization
    private void Awake()
    {
		instance = this;
		pathfinding = GetComponent<Pathfinding>();
	}
    #endregion

    #region Update
    private void Update()
    {
		if (results.Count > 0)
        {
			int itemsInQueue = results.Count;
			lock (results)
            {
				for (int i = 0; i < itemsInQueue; i++)
                {
					PathResult result = results.Dequeue ();
					result.callback (result.pathNodes, result.path, result.success);
				}
			}
		}
	}
    #endregion

    #region Management Requests
    public static void RequestPath(PathRequest request)
    {
		ThreadStart threadStart = delegate {
			instance.pathfinding.FindPath (request, instance.FinishedProcessingPath);
		};
		threadStart.Invoke ();
	}

	public void FinishedProcessingPath(PathResult result)
    {
		lock (results)
        {
			results.Enqueue (result);
		}
	}
    #endregion


}

#region Result + Request Structures
public struct PathResult
{
    public List<Node> pathNodes;
	public Vector3[] path;
	public bool success;
	public Action<List<Node>, Vector3[], bool> callback;

	public PathResult (List<Node> pathNodes, Vector3[] path, bool success, Action<List<Node>, Vector3[], bool> callback)
	{
        this.pathNodes = pathNodes;
		this.path = path;
		this.success = success;
		this.callback = callback;
	}

}

public struct PathRequest
{
	public Vector3 pathStart;
	public Vector3 pathEnd;
	public Action<List<Node>, Vector3[], bool> callback;

	public PathRequest(Vector3 _start, Vector3 _end, Action<List<Node>, Vector3[], bool> _callback) {
		pathStart = _start;
		pathEnd = _end;
		callback = _callback;
	}

}
#endregion
