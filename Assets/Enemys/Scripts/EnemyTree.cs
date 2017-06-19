using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTree : MonoBehaviour
{
    public float damage;
    private List<UnitHealth> units;

    private void Start()
    {
        units = new List<UnitHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        UnitHealth unit = other.GetComponent<UnitHealth>();
        if (unit != null && !units.Contains(unit))
            units.Add(unit);

        if (other.transform.name == "DangerZone(Clone)")
        {
            DealDamage(damage);
            Destroy(gameObject, 3f);
            Destroy(other.transform.gameObject);
            Destroy(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        UnitHealth unit = other.GetComponent<UnitHealth>();
        if (unit != null && units.Contains(unit))
            units.Remove(unit);
    }

    public void DealDamage(float damage)
    {
        foreach (UnitHealth unit in units)
            unit.Hit(damage);
    }
}
