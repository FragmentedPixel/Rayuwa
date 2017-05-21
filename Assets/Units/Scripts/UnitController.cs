﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Agent))]
public class UnitController : MonoBehaviour
{
    public MeshRenderer debugCube;
    public Vector3 oldDestination;

    #region Variabiles

    #region Targeting
    [HideInInspector] public Agent agent;
    [HideInInspector] public Transform target;
    private List<Transform> targetsInRange = new List<Transform>();
    #endregion

    #region Parameters
    [HideInInspector] public bool battleStarted = false;

    [Header("Attack")]
    public float fightRange = 3f;
    public float fightSpeed = 1f;
	public float fightDmg = 10f;
    #endregion

    #region States
    public iUnitState currentState;

    public AggroState aggroState;
    public BattleState battleState;
    public FightState fightState;
    public IdleState idleState;
    #endregion

    #endregion

    #region Initialization
    private void Awake()
    {
        aggroState = new AggroState(this);
        battleState = new BattleState(this);
        fightState = new FightState(this);
        idleState = new IdleState(this);

        currentState = idleState;
    }
    private void Start()
    {
        agent = GetComponent<Agent>();
        agent.MoveToDestination(GameObject.Find("Castel").transform.position);
    }
    #endregion

    #region CurrentState
    private void Update()
    {
        currentState.Update();
    }
    private void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth == null || targetsInRange.Contains(enemyHealth.transform))
            return;

        targetsInRange.Add(enemyHealth.transform);
        currentState.OnTriggerEnter(enemyHealth.transform);
    }
    private void OnTriggerExit(Collider other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth == null || !targetsInRange.Contains(enemyHealth.transform))
            return;
        else
            targetsInRange.Remove(enemyHealth.transform);
    }
    #endregion

    #region Methods
    public void StartBattle()
    {
        battleStarted = true;
        currentState = battleState;
        agent.Resume();
    }
    public void LookAtTarget()
    {
        Vector3 lookPoint = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.LookAt(lookPoint);
    }
    public void UpdateTarget()
    {
        float distance = int.MaxValue;
        for(int i = 0; i < targetsInRange.Count; i++)
        {
            if (targetsInRange[i] == null)
            {
                targetsInRange.Remove(targetsInRange[i]);
                i--;
            }
            else
            {

                float newDistance = Vector3.Distance(transform.position, targetsInRange[i].position);
                if (newDistance < distance)
                {
                    target = targetsInRange[i];
                    distance = newDistance;
                }
            }
        }

        if ((target == null) && (!battleStarted))
            currentState.ToIdleState();
        else if (target == null)
        {
            if(oldDestination != Vector3.zero)
            agent.MoveToDestination(oldDestination);
            currentState.ToBattleState();
        }
        else if (distance > fightRange)
            currentState.ToAggroState();
        else
            currentState.ToFightState();

    }
    public float DistanceToTarget()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        return distance;
    }
    public void CheckNewTarget(Transform newTarget)
    {
        oldDestination = agent.destination;
        agent.MoveToDestination(newTarget.position);

        target = newTarget;

        float distance = DistanceToTarget();
        if (distance < fightRange)
            currentState.ToFightState();
        else
            currentState.ToAggroState();
    }
    #endregion

}
