using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public float speed=80/36f;
	void Start () 
	{
		
	}

	void Update () 
	{
		Vector3 force = new Vector3 (transform.position.x,transform.position.y,transform.position.z);
		force.z += speed * Time.deltaTime; 
		transform.position = force;
	}
}
