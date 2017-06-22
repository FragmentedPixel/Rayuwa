using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalDefense : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        UnitHealth unit = other.GetComponent<UnitHealth>();

        if (unit == null)
            return;

        EnemyHealth[] enemies = FindObjectsOfType<EnemyHealth>();
        foreach (EnemyHealth enemy in enemies)
            enemy.Hit(0, unit.transform);
    }

}
