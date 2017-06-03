using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLauncher : MonoBehaviour
{
    public Transform target;
    public float height = 25f;
    public float gravity = -18;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Launch();
    }

    private void Launch()
    {
        Physics.gravity = Vector3.up * gravity;
        rb.useGravity = true;
        rb.velocity = CalculateLaunchVelocity();
    }

    private Vector3 CalculateLaunchVelocity()
    {
        float displacementY = target.position.y - transform.position.y;
        Vector3 DisplacementXZ = new Vector3(target.position.x - transform.position.x, 0f, target.position.z - transform.position.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
        Vector3 velocityXZ = DisplacementXZ / (Mathf.Sqrt(-2 * height / gravity) + Mathf.Sqrt(2*(displacementY - height) / gravity));

        return velocityXZ + velocityY;
    }

}
