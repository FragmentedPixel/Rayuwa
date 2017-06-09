using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLauncher : MonoBehaviour
{
    public GameObject dangerZone;
    public float gravity = -18;
    public float maxRandomRotation;

    private Transform target;
    private Transform tree;

    public float height = 25f;
    private Rigidbody rb;

    public void Launch(Transform _tree, Transform _target)
    {
        tree = _tree;
        target = _target;

		Instantiate(dangerZone, target.position + (-target.forward * 2f), Quaternion.identity);

        Physics.gravity = Vector3.up * gravity;
        rb = tree.GetComponent<Rigidbody>();

        rb.useGravity = true;
        rb.velocity = CalculateLaunchVelocity();
        rb.AddTorque(Random.Range(0f, maxRandomRotation), Random.Range(0f, maxRandomRotation), Random.Range(0f, maxRandomRotation));
    }
    private Vector3 CalculateLaunchVelocity()
    {
        float displacementY = target.position.y - tree.position.y;
        Vector3 DisplacementXZ = new Vector3(target.position.x - tree.position.x, 0f, target.position.z - tree.position.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
        Vector3 velocityXZ = DisplacementXZ / (Mathf.Sqrt(-2 * height / gravity) + Mathf.Sqrt(2*(displacementY - height) / gravity));
        return velocityXZ + velocityY;
    }

}
