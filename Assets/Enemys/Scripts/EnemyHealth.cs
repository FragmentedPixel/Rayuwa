using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
	public float MaxHealth=500;
	private float currentHealth;
	// Use this for initialization
	void Start () 
	{
		currentHealth = MaxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Hit(float damage)
	{
		currentHealth -= damage;
		if (currentHealth <= 0)
		{
			Destroy (transform.parent.gameObject);
		}
		Debug.Log (currentHealth);
	}
}
