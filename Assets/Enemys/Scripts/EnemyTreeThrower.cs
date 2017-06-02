using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Agent))]
public class EnemyTreeThrower : MonoBehaviour
{
    public GameObject treeHolded;

    private GameObject targetTree;
    private Agent agent;

    private void Start()
    {
        agent = GetComponent<Agent>();
    }

    

    private void FindClosestTree()
    {
        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
        float distance = int.MaxValue;

        foreach (GameObject tree in trees)
        {
            float newDistance = DistanceToObject(transform, tree.transform);
            if (newDistance < distance)
                targetTree = tree;
        }
    }

    private float DistanceToObject(Transform t1, Transform t2)
    {
        Vector3 startPos = new Vector3(t1.position.x, 0f, t1.position.z);
        Vector3 finalPos = new Vector3(t2.position.x, 0f, t2.position.z);

        return Vector3.Distance(startPos, finalPos);
    }
}
