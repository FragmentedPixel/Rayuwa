using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Agent))]
public class EnemyController : MonoBehaviour
{

    #region Variabiles

    #region Targeting
    [HideInInspector] public Agent agent;
    [HideInInspector] public Transform target;
    private List<Transform> targetsInRange = new List<Transform>();
    #endregion

    #region Parameters
    [Header("Patroling")]
    public Transform WayPointParent;

	[Header("Attack")]
    public float attackRange = 3f;
    public float attackSpeed = 1f;
    public float attackDmg = 10f;
    #endregion

    #region States
    public iEnemyState currentState;
    public AttackState attackState;
    public ChaseState chaseState;
    public PatrolState patrolState;

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
        agent = GetComponent<Agent>();
    }
    #endregion

    #region CurrentState Methods
    private void Update()
	{
		currentState.Update ();
	}

	private void OnTriggerEnter(Collider collider)
	{
		currentState.OnTriggerEnter (collider);
	}
    #endregion

    #region Methods

    #endregion
}
