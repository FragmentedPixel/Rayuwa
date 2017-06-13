using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderController : MonoBehaviour {

    public float movementSpeed = 2f;
    public float rotationSpeed = 3f;


	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        GetComponent<Rigidbody>().velocity = new Vector3(horizontal, 0, 0).normalized * Time.fixedDeltaTime * movementSpeed; 
	}
}
