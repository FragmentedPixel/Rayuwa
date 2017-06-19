using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathTrap : MonoBehaviour
{
    [Header("Specifics")]
    public GameObject enemy;
    public int count;

    public void SpawnSmallGolems()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        EnemyController controller = GetComponentInParent<EnemyController>();

        Transform wayPointsParent = controller.wayPointsParent;
        List<Transform> targetsInRange = controller.targetsInRange;
        Vector3 spawnPosition = controller.transform.position;

        for (int i = 0; i < count; i++)
        {
            Vector3 spawnOffSet = Random.insideUnitSphere * 2f;
            spawnOffSet.y = 0f;

            GameObject spawnedEnemy = Instantiate(enemy, spawnPosition + spawnOffSet, transform.rotation, transform.parent.parent);

            spawnedEnemy.GetComponent<Collider>().enabled = false;
            spawnedEnemy.GetComponent<Collider>().enabled = true;

            spawnedEnemy.GetComponent<EnemyController>().wayPointsParent = wayPointsParent;
            spawnedEnemy.GetComponent<EnemyController>().targetsInRange = targetsInRange;
        }

        try { FindObjectOfType<UnitsManager>().ResetUnitsColliders(); }
        catch { }
    }
}
