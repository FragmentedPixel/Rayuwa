using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Agent))]
public class EnemyTreeThrower : MonoBehaviour
{
    #region Variabiles
    //Paramters
    public float pickUpRange;
    public float attackSpeed;
    private float waitTime;

    //copaco
    public GameObject treeHolded;
    private GameObject treeTargeted;
    private List<GameObject> allTrees;

    //Componente
    private Agent agent;
    private TreeLauncher launcher;
    #endregion

    #region Initialization
    private void Start()
    {
        agent = GetComponent<Agent>();
        launcher = GetComponent<TreeLauncher>();

        allTrees = GameObject.FindGameObjectsWithTag("Tree").ToList();
    }
    #endregion

    #region Update
    private void Update()
    {
        if (treeHolded != null)
        {
            if (waitTime >= attackSpeed)
                Throw();
            else
                Wait();
        }
        else if (treeTargeted != null)
        {
            if (DistanceToObject(transform, treeTargeted.transform) < pickUpRange)
                PickUp();
            else
                GoTowardsTargeted();
        }
        else
            FindClosestTree();
    }
    #endregion

    #region Methods
    private void Throw()
    {
        Transform target = FindTarget();
        launcher.Launch(treeHolded.transform, target);

        allTrees.Remove(treeHolded);
        waitTime = 0f;
    }
    private void Wait()
    {
        waitTime += Time.deltaTime;
    }
    private void PickUp()
    {
        treeHolded = treeTargeted;
    }
    private void GoTowardsTargeted()
    {
        agent.MoveToDestination(treeTargeted.transform.position);
    }
    private void FindClosestTree()
    {
        float distance = int.MaxValue;

        foreach (GameObject tree in allTrees)
        {
            float newDistance = DistanceToObject(transform, tree.transform);
            if (newDistance < distance)
            {
                distance = newDistance;
                treeTargeted = tree;
            }
        }
    }
    private Transform FindTarget()
    {
        return GameObject.Find("Castel").transform;
    }
    #endregion

    #region Utility
    private float DistanceToObject(Transform t1, Transform t2)
    {
        Vector3 startPos = new Vector3(t1.position.x, 0f, t1.position.z);
        Vector3 finalPos = new Vector3(t2.position.x, 0f, t2.position.z);

        return Vector3.Distance(startPos, finalPos);
    }
    #endregion
}
