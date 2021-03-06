﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Agent))]
public abstract class  EnemyController : MonoBehaviour
{
    #region Variabiles

    #region Targeting + Components
    [HideInInspector] public Animator anim;
    [HideInInspector] public Agent agent;
    [HideInInspector] public AudioSource audioS;
    public Transform target;
    [HideInInspector] public Transform lastTarget;
    [HideInInspector] public List<Transform> targetsInRange = new List<Transform>();
    #endregion

    #region Parameters
    [Header("Patroling")]
    public Transform wayPointsParent;

	[Header("Attack")]
    public float attackRange = 3f;
    public float attackSpeed = 1f;
    public float attackDmg = 10f;
    public AudioClip attackSound;
    public float targetTreshold = 2f;
    public float agroRange = 10;

    [Header("Ammo")]
    public int maxAmmo = 10;
    public int currentAmmo;
    #endregion

    #region States
    public iEnemyState currentState;
    public AttackState attackState;
    public ChaseState chaseState;
    public PatrolState patrolState;

    #endregion

    #region Ammo
    public Transform refillPlace;

    #endregion

    #endregion

    #region Initialization
    private void Awake()
	{
		attackState = new AttackState (this);
		chaseState = new ChaseState (this);
		patrolState = new PatrolState (this);

        currentState = patrolState;
    }

    

    private void Start()
    {
        if (wayPointsParent == null)
            target = transform;
        anim = GetComponent<Animator>();
        agent = GetComponent<Agent>();
        audioS = GetComponent<AudioSource>();
        audioS.volume = PlayerPrefsManager.GetMasterVolume();

        currentAmmo = maxAmmo;
    }
    #endregion

    #region CurrentState
    private void Update()
	{
		currentState.Update ();
	}

	private void OnTriggerEnter(Collider other)
	{
        UnitHealth unitHealth = other.GetComponent<UnitHealth>();
        if (unitHealth == null || targetsInRange.Contains(unitHealth.transform))
            return;

        targetsInRange.Add(unitHealth.transform);
        if (!Ammo())
            return;
        currentState.OnTriggerEnter(unitHealth.transform);
	}

    private void OnTriggerExit(Collider other)
    {
        UnitHealth unitHealth = other.GetComponent<UnitHealth>();
        if (unitHealth == null || !targetsInRange.Contains(unitHealth.transform))
            return;
        else
            targetsInRange.Remove(unitHealth.transform);
    }
    #endregion

    #region Methods
    public void LookAtTarget()
    {
        Vector3 lookPoint = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.LookAt(lookPoint);
    }

    public void UpdateTarget()
    {
        float distance = int.MaxValue;
        for (int i = 0; i < targetsInRange.Count; i++)
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

        if (target == null)
        {
            if (lastTarget != null)
                target = lastTarget;
            else
                currentState.ToPatrolState();
        }
        else if (distance > attackRange)
            currentState.ToChaseState();
        else
            currentState.ToAttackState();

    }
    public float DistanceToTarget()
    {
        Vector3 tranPos = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 targetPos = new Vector3(target.position.x, 0f, target.position.z);

        float distance = Vector3.Distance(tranPos, targetPos);
        return distance;
    }

    public abstract void AttackTarget();

    public abstract bool Ammo();

    #endregion
}
