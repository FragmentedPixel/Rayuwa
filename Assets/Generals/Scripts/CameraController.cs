using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public float speed = 10.0F;
	private Vector3 startPosition;
	public float Z_Boundary=50f;
    [Range(0,0.5f)]
    public float screenPercent = 0.1f;
    private float screenMoveSize;
	void Start () 
	{
        screenPercent = PlayerPrefsManager.GetScrollBoundray();
		startPosition = transform.position;
        screenMoveSize = Screen.width* screenPercent;
	}
		


	void Update() 
	{
		float movement = Input.GetAxis("Horizontal");

        if (movement == 0)
            if (Input.mousePosition.x < screenMoveSize)
                movement = -1 * Mathf.InverseLerp(screenMoveSize, 0, Input.mousePosition.x);
            else if (Input.mousePosition.x > Screen.width - screenMoveSize)
                movement = 1 * Mathf.InverseLerp(Screen.width - screenMoveSize, Screen.width, Input.mousePosition.x);

        movement *= Time.deltaTime * speed;
        movement += transform.position.z;

		if(movement>startPosition.z&&movement<startPosition.z+Z_Boundary)
			transform.position += new Vector3 (0,0,movement-transform.position.z);
	}
}
