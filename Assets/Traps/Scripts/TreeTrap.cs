using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTrap : Trap
{
    #region Variabiles
    [Header("Tree")]
    public GameObject[] trees;
    public float angle = 5f;
    public float duration = .5f;

    [Header("Trap")]
    public float damage;

    private bool triggered = false;
    #endregion

    #region Triggers
    private void Start()
    {
        TriggerTrap();
    }
    private void OnTriggerEnter(Collider other)
    {
        UnitHealth unit = other.GetComponent<UnitHealth>();

        if (!triggered && unit != null)
            TriggerTrap();
    }

    private void TriggerTrap()
    {
        triggered = true;
        gameObject.layer = 9;
        StartCoroutine(ColapseAllTrees());
    }

    #endregion

    #region Animations
    private IEnumerator ColapseAllTrees()
    {
        yield return StartCoroutine(ColapseTree(trees[0], 1, 1));
        yield return StartCoroutine(ColapseTree(trees[1], 1, -1));
        yield return StartCoroutine(ColapseTree(trees[2], -1, -1));
        yield return StartCoroutine(ColapseTree(trees[3], -1, 1));

        FindObjectOfType<Grid>().ReCalculateGird();
    }
    private IEnumerator ColapseTree(GameObject tree, int a, int b)
    {
        float currentTime = 0f;
        EnemyTree enemyTree = tree.AddComponent<EnemyTree>();
        enemyTree.damage = damage;

        while(currentTime < duration)
        {
            tree.transform.Rotate(angle * a * Time.deltaTime, 0f, angle * b * Time.deltaTime);    
        
            currentTime += Time.deltaTime;
            yield return null;
        }

        Destroy(enemyTree);

        yield break;
    }
    #endregion
}
