using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ionut : MonoBehaviour {
    public float groundHeight;
    public LayerMask walkableMask;
    public Transform parentTransform;
    // Use this for initialization
    void Start()
    {
        parentTransform = GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, 15, walkableMask))
        {
            Debug.DrawLine(transform.position, raycastHit.point,Color.red);
            Vector3 desiredTransform = parentTransform.position;
            desiredTransform.y += -(transform.position.y - raycastHit.point.y) + groundHeight; 
           transform.parent.position = desiredTransform;
        }
    }
}
