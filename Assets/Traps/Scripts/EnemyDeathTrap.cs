using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathTrap : MonoBehaviour
{
    [Header("Specifics")]
    public GameObject[] enemies;

    public void OnDestroy()
    {
        SpawnEnemies();
    }
    private void SpawnEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true);
        }

        Destroy(gameObject);
    }
}
