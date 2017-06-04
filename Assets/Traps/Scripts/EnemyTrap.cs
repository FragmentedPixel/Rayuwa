using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrap : Trap
{

    [Header("Specifics")]
    public GameObject[] enemies;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Unit"))
            SpawnEnemies();
    }
    private void SpawnEnemies()
    {
        foreach(GameObject enemy in enemies)
        {
            enemy.SetActive(true);
        }

        Destroy(gameObject);
    }
}
