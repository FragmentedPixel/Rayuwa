using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public float speed = 10.0F;
	private Vector3 startPosition;
	public float Z_Boundary=50f;
	void Start () 
	{
		startPosition = transform.position;
	}
		


	void Update() 
	{
		float movement = Input.GetAxis("Horizontal") * speed;
		movement *= Time.deltaTime;
		movement += transform.position.z;

		if(movement>startPosition.z&&movement<startPosition.z+Z_Boundary)
			transform.position += new Vector3 (0,0,movement-transform.position.z);
	}
}
