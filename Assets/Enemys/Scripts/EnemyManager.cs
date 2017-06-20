using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class EnemyManager : MonoBehaviour
{
    //private List<Transform> enemyList= new List<Transform>();
    public static EnemyManager instance;

    #region Initialization
    private void Start()
    {
        instance = this;
        EnemyHealth[] enemies = FindObjectsOfType<EnemyHealth>();
        foreach (EnemyHealth enemey in enemies)
            SetSightAndRange(enemey);
    }

    public List<Transform> enemyList
    {
        get
        {
            List<Transform> list = new List<Transform>();
            EnemyHealth[] enemies = FindObjectsOfType<EnemyHealth>();
            foreach (EnemyHealth enemy in enemies)
                if(enemy.GetComponentInParent<EnemyController>() != null)
                    list.Add(enemy.transform);

            return list;
        }
    }

    private void SetSightAndRange(EnemyHealth enemy)
    {   
        EnemyHealth enemyHealth = enemy.GetComponentInChildren<EnemyHealth>();
        enemyHealth.transform.parent.GetComponentInChildren<Outline>().enabled = false;

        EnemyController enemyController = enemy.GetComponentInParent<EnemyController>();

        Transform range = enemyHealth.transform.GetChild(0);
        Transform sight = enemyHealth.transform.GetChild(1);

        if (enemyController == null)
        {
            Destroy(range.gameObject);
            sight.transform.localScale = enemyHealth.GetComponentInParent<SphereCollider>().radius * Vector3.one;
            return;
        }
        //enemyList.Add(enemy.transform);

        range.transform.localScale = Vector3.one * enemyController.attackRange;
        sight.transform.localScale = Vector3.one * enemyController.GetComponent<SphereCollider>().radius ;

    }
    #endregion
}
