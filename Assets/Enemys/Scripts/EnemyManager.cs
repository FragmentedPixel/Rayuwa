using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public List<Transform> enemyList= new List<Transform>();
    public static EnemyManager instance;

    #region Initialization
    private void Start()
    {
        instance = this;
        foreach (Transform child in transform)
            SetSightAndRange(child);
    }

    private void SetSightAndRange(Transform enemy)
    {
        
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        EnemyHealth enemyHealth = enemy.GetComponentInChildren<EnemyHealth>();

        if (enemyController == null)
            return;
        enemyList.Add(enemy);
        Transform range = enemyHealth.transform.GetChild(0);
        Transform sight = enemyHealth.transform.GetChild(1);

        range.transform.localScale = Vector3.one * enemyController.attackRange;
        sight.transform.localScale = Vector3.one * enemyController.GetComponent<SphereCollider>().radius ;
    }
    #endregion
}
