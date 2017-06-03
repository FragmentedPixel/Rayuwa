using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTree : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        UnitHealth unitHealth = other.GetComponent<UnitHealth>();
        unitHealth.Hit(damage, null);
    }
}
