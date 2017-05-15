﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Agent))]
public class UnitController : MonoBehaviour
{
    public MeshRenderer debugCube;

    #region Variabiles
    #region States
    public iUnitState currentState;

    public AggroState aggroState;
    public BattleState battleState;
    public FightState fightState;
    public IdleState idleState;
    #endregion

    #region Targeting
    public Transform target;
    public List<Transform> targetsInRange = new List<Transform>();
    public Agent agent;
    #endregion

    #region Parameters
    public bool battleStarted = false;
    public float fightRange = 5f;
    public float fightCooldown = 1f;
	public float fightDamage = 10f;
    #endregion
    #endregion

    #region Start
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
        agent.SetNewDestination(GameObject.Find("Castel").transform);
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
            if(targetsInRange[i] == null)
            {
                targetsInRange.Remove(targetsInRange[i]);
                i--;
            }

            float newDistance = Vector3.Distance(transform.position, targetsInRange[i].position);
            if(newDistance < distance)
            {
                target = targetsInRange[i];
                distance = newDistance;
            }
        }

        if ((target == null) && (!battleStarted))
            currentState.ToIdleState();
        else if(target == null)
            currentState.ToBattleState();
        else if (distance > fightRange)
            currentState.ToAggroState();
        else
            currentState.ToFightState();

    }
    public void CheckNewTarget(Transform newTarget)
    {
        agent.SetNewDestination(newTarget);
        target = newTarget;

        float distance = Vector3.Distance(transform.position, target.position);
        if (distance < fightRange)
            currentState.ToFightState();
        else
            currentState.ToAggroState();
    }
    #endregion

}
