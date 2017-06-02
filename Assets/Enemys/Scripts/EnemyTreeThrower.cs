using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Agent))]
public class EnemyTreeThrower : MonoBehaviour
{
    #region Variabiles
    public GameObject treeHolded;
    public float pickUpRange;
    public float attackSpeed;

    private float waitTime;
    private GameObject treeTargeted;
    private Agent agent;
    private bool isTimeToAttack;
    #endregion

    #region Initialization
    private void Start()
    {
        agent = GetComponent<Agent>();
    }
    #endregion

    #region Update
    private void Update()
    {
        if (treeHolded != null)
        {
            Debug.Log("Are copac in mana");

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
        Debug.Log("Inceput animatie aruncat copaci.");
        Destroy(treeHolded);
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
        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
        float distance = int.MaxValue;

        foreach (GameObject tree in trees)
        {
            float newDistance = DistanceToObject(transform, tree.transform);
            if (newDistance < distance)
            {
                distance = newDistance;
                treeTargeted = tree;
            }
        }
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
