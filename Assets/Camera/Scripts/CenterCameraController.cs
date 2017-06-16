using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterCameraController : MonoBehaviour
{
    private Vector3 startPosition;
    [Header("Speeds")]
    public float speed = 10.0F;
    public float Yspeed = 100f;

    [Header("Boundaries")]
    public float X_Boundary = 50f;
    public float Y_Boundary = 10f;
    public float Z_Boundary = 50f;
    public float lookAtOffset = 25f;

    [Header("LookAt")]
    public Transform lookAt;

    void Start () 
	{
        if (startPosition == Vector3.zero)
            startPosition = transform.position;
        transform.position -= new Vector3(-Z_Boundary/2, 0, 0); 
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

		float movementH = Input.GetAxis("Vertical");
        float movementV = Input.GetAxis("Horizontal");
        float movementY = (PlayerPrefsManager.GetInverseScroll() ? (-1) : (1) ) * Input.GetAxis("Mouse ScrollWheel");

        Debug.Log(movementY);

        movementH *= Time.deltaTime * speed;
        movementH += transform.position.z;
        movementV *= Time.deltaTime * speed;
        movementV += transform.position.x;
        movementY *= Time.deltaTime * Yspeed;
        movementY += transform.position.y;

        movementH = Mathf.Clamp(movementH, startPosition.z, startPosition.z + Z_Boundary);
        movementV = Mathf.Clamp(movementV, startPosition.x - X_Boundary, startPosition.x);
        movementY = Mathf.Clamp(movementY, startPosition.y - Y_Boundary, startPosition.y);

        transform.position = new Vector3(movementV , movementY, movementH );

        transform.LookAt(lookAt);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x+lookAtOffset,transform.rotation.eulerAngles.y,transform.rotation.eulerAngles.z);
            
        
	}
}
