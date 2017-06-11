using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathTrap : MonoBehaviour
{
    [Header("Specifics")]
    public GameObject enemy;
    public int count;

    public void OnDestroy()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnOffSet = Random.insideUnitSphere * 2f;
            spawnOffSet.y = 0f;

            Instantiate(enemy, transform.position + spawnOffSet, transform.rotation, transform.parent);
        }
    }
}
