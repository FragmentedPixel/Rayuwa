using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float minX=-22;
    public float maxX=22;
    public float minZ=18;
    public float maxZ=60;
    public bool hasTarget = false;
    public float speed=20;
    private Vector3 target;
    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void  FixedUpdate ()
    {
        transform.Rotate(0,0.1f,0);
        float step = speed * Time.fixedDeltaTime;
        if (!hasTarget)
        {
            hasTarget = true;
            float x = Random.Range(minX,maxX);
            float z = Random.Range(minZ, maxZ);
            target = new Vector3(x, transform.position.y, z);
            
        }
        if (target == transform.position)
            hasTarget = false;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }

}
