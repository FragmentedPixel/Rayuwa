using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class EnemyManager : MonoBehaviour
{
    public List<Transform> enemyList= new List<Transform>();
    public static EnemyManager instance;

    #region Initialization
    private void Start()
    {
        instance = this;
        EnemyHealth[] enemies = FindObjectsOfType<EnemyHealth>();
        foreach (EnemyHealth enemey in enemies)
            SetSightAndRange(enemey);
    }

    private void SetSightAndRange(EnemyHealth enemy)
    {   
        EnemyHealth enemyHealth = enemy.GetComponentInChildren<EnemyHealth>();
        enemyHealth.transform.parent.GetComponentInChildren<Outline>().enabled = false;

        EnemyController enemyController = enemy.GetComponentInParent<EnemyController>();

        if (enemyController == null)
            return;

        enemyList.Add(enemy.transform);
        Transform range = enemyHealth.transform.GetChild(0);
        Transform sight = enemyHealth.transform.GetChild(1);

        range.transform.localScale = Vector3.one * enemyController.attackRange;
        sight.transform.localScale = Vector3.one * enemyController.GetComponent<SphereCollider>().radius ;

    }
    #endregion
}
