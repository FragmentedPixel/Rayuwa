using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCameraController : MonoBehaviour {
	
	private Vector3 startPosition;
    private float screenMoveSize;

    [Header("Speeds")]
    public float speed = 10.0F;
    public float Yspeed = 100.0F;
    public float rot_speed = 10.0F;
    
    [Header("Boundaries")]
    [Range(0, 0.5f)]
    public float screenPercent = 0.1f;
    public float Z_Boundary = 50f;
    public float X_Boundary = 20f;
    public float Y_Boundary = 20f;
    [Range(0, 360f)]
    public float Y_Rot_Min = 240f;
    [Range(0, 360f)]
    public float Y_Rot_Max = 300f;

    void Start () 
	{
        screenPercent = PlayerPrefsManager.GetScrollBoundray();
		startPosition = transform.position;
        screenMoveSize = Screen.width* screenPercent;
	}
		
    public void Clamp(float zPosition)
    {
        Vector3 newposition = transform.position;
        newposition.z = Mathf.Clamp(zPosition, startPosition.z, startPosition.z + Z_Boundary);
        transform.position = newposition;
    }

	public void Update() 
	{
        
        float movementH = Input.GetAxis("Horizontal");
        float movementV = -Input.GetAxis("Vertical");
        float movementY = (PlayerPrefsManager.GetInverseScroll() ? (-1) : (1)) * Input.GetAxis("Mouse ScrollWheel"); 

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

        float rotate=0;
        if (Input.mousePosition.x < screenMoveSize)
            rotate = -1 * Mathf.InverseLerp(screenMoveSize, 0, Input.mousePosition.x);
        else if (Input.mousePosition.x > Screen.width - screenMoveSize)
            rotate = 1 * Mathf.InverseLerp(Screen.width - screenMoveSize, Screen.width, Input.mousePosition.x);

        if (Input.GetKey(KeyCode.Q))
            rotate = -1;
        else if (Input.GetKey(KeyCode.E))
            rotate = 1;

        if (rotate != 0)
        {
            rotate *= Time.deltaTime * rot_speed;
            rotate += transform.rotation.eulerAngles.y;
            if (rotate< Y_Rot_Max&&rotate>Y_Rot_Min)
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, rotate, transform.rotation.eulerAngles.z);
        }
	}
}
