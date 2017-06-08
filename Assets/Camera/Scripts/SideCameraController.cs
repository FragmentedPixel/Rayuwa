using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCameraController : MonoBehaviour {
	
	private Vector3 startPosition;
    private float screenMoveSize;

    public bool isEnabled;

    [Header("Speeds")]
    public float speed = 10.0F;
    public float rot_speed = 10.0F;
    
    [Header("Boundaries")]
    [Range(0, 0.5f)]
    public float screenPercent = 0.1f;
    public float Z_Boundary = 50f;
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
        
        float movement = Input.GetAxis("Horizontal");
        if (movement != 0)
        {
            movement *= Time.deltaTime * speed;
            movement += transform.position.z;

            if (movement > startPosition.z && movement < startPosition.z + Z_Boundary)
                transform.position += new Vector3(0, 0, movement - transform.position.z);
        }

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
