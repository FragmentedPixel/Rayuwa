using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public iEnemyState currentState;
	public Transform WayPointParent;
	public Attack attack;
	public Chase chase;
	public Patrol patrol;
	public Agent agent;
	public Transform target;
	public float attackDistance = 1;

	private void Awake()
	{
		attack = new Attack (this);
		chase = new Chase (this);
		patrol = new Patrol (this);

		currentState = patrol;


	}


	private void Update()
	{
		currentState.Update ();
	}

	private void OnTriggerEnter(Collider collider)
	{
		currentState.OnTriggerEnter (collider);
	}
}
