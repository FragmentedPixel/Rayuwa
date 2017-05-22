using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterCameraController : MonoBehaviour
{
    private Vector3 startPosition;
    private float screenMoveSize;
    [Header("Speeds")]
    public float speed = 10.0F;

    [Header("Boundaries")]
    [Range(0, 0.5f)]
    public float screenPercent = 0.0f;
    public float Z_Boundary = 50f;
    public float lookAtOffset = 25f;

    [Header("LookAt")]
    public Transform lookAt;

    void Start () 
	{
        screenPercent = PlayerPrefsManager.GetScrollBoundray();
		startPosition = transform.position;
        screenMoveSize = Screen.width* screenPercent;

        transform.LookAt(lookAt);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + lookAtOffset, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
		


	void Update() 
	{
		float movement = Input.GetAxis("Vertical");

       /*
        if (Input.mousePosition.x < screenMoveSize)
            movement = -1 * Mathf.InverseLerp(screenMoveSize, 0, Input.mousePosition.x);
        else if (Input.mousePosition.x > Screen.width - screenMoveSize)
            movement = 1 * Mathf.InverseLerp(Screen.width - screenMoveSize, Screen.width, Input.mousePosition.x);
        */
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
