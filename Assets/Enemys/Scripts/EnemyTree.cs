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
            Destroy(gameObject, 3f);
            Destroy(other.transform.gameObject);
            Destroy(this);
        }
    }
}
