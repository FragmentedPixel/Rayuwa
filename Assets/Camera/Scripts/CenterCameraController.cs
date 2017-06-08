using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterCameraController : MonoBehaviour
{
    private Vector3 startPosition;
    [Header("Speeds")]
    public float speed = 10.0F;

    [Header("Boundaries")]
    public float Z_Boundary = 50f;
    public float lookAtOffset = 25f;

    [Header("LookAt")]
    public Transform lookAt;

    void Start () 
	{
        if (startPosition == Vector3.zero)
            startPosition = transform.position;

        transform.LookAt(lookAt);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + lookAtOffset, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
		
    public void Clamp(float zPosition)
    {
        if(startPosition==Vector3.zero)
            startPosition = transform.position;

        Vector3 position = transform.position;
        position.z = Mathf.Clamp(zPosition, startPosition.z, startPosition.z + Z_Boundary);
        transform.position = position;

        transform.LookAt(lookAt);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + lookAtOffset, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

	void Update() 
	{

		float movement = Input.GetAxis("Vertical");

        movement *= Time.deltaTime * speed;
        movement += transform.position.z;

        if (movement > startPosition.z && movement < startPosition.z + Z_Boundary)
        {
            transform.position += new Vector3(0, 0, movement - transform.position.z);
            transform.LookAt(lookAt);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x+lookAtOffset,transform.rotation.eulerAngles.y,transform.rotation.eulerAngles.z);
            
        }
        
	}
}
