using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Agent))]
public class EnemyController : MonoBehaviour
{
	
    #region Variabiles

    [HideInInspector] public Agent agent;

    [Header("Patroling")]
    public Transform WayPointParent;
    
    [Header("Targeting")]
    public Transform target;
	public float attackDistance = 1;

	[Header("Attack")]
	public float attackDmg = 5;
	public float attackSpeed= 1*1000;

    #region States
    public iEnemyState currentState;
    public AttackState attack;
    public ChaseState chase;
    public PatrolState patrol;

    #endregion

    #endregion

    #region Initialization
    private void Awake()
	{
		attack = new AttackState (this);
		chase = new ChaseState (this);
		patrol = new PatrolState (this);
	}

    private void Start()
    {
        agent = GetComponent<Agent>();
        currentState = patrol;
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
