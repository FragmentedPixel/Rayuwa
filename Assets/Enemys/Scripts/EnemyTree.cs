using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTree : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        UnitHealth unitHealth = other.GetComponent<UnitHealth>();

        if (unitHealth != null)
            unitHealth.Hit(damage, null);

        if (other.transform.name == "DangerZone(Clone)")
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            Destroy(rb);
            Destroy(gameObject, 3f);
            Destroy(other.transform.gameObject);
        }
    }
}
