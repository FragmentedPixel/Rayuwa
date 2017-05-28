using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region Initialization
    private void Start()
    {
        foreach (Transform child in transform)
            SetSightAndRange(child);
    }

    private void SetSightAndRange(Transform enemy)
    {
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        EnemyHealth enemyHealth = enemy.GetComponentInChildren<EnemyHealth>();

        if (enemyController == null)
            return;

        Transform range = enemyHealth.transform.GetChild(0);
        Transform sight = enemyHealth.transform.GetChild(1);

        range.transform.localScale = Vector3.one * enemyController.attackRange;
        sight.transform.localScale = Vector3.one * enemyController.GetComponent<SphereCollider>().radius * 2;
    }
    #endregion
}
