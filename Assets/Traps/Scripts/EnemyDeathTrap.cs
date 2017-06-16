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
        EnemyController controller = GetComponentInParent<EnemyController>();

        for (int i = 0; i < count; i++)
        {
            Vector3 spawnOffSet = Random.insideUnitSphere * 2f;
            spawnOffSet.y = 0f;

            GameObject spawnedEnemy = Instantiate(enemy, transform.position + spawnOffSet, transform.rotation, transform.parent);

            spawnedEnemy.GetComponent<Collider>().enabled = false;
            spawnedEnemy.GetComponent<Collider>().enabled = true;

            spawnedEnemy.GetComponent<EnemyController>().wayPointsParent = controller.wayPointsParent;
            spawnedEnemy.GetComponent<EnemyController>().targetsInRange = controller.targetsInRange;
        }
        try { FindObjectOfType<UnitsManager>().ResetUnitsColliders(); }
        catch { }
    }
}
