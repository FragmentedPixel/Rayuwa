using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTrap : MonoBehaviour
{
    public List<UnitHealth> units = new List<UnitHealth>();
    public GameObject[] trees;

    public int unitsCount = 3;
    public float angle = 5f;
    public float duration = .5f;

    private bool triggered = false;

    #region Triggers
    private void OnTriggerEnter(Collider other)
    {
        UnitHealth unit = other.transform.GetComponent<UnitHealth>();

        if (unit != null && !units.Contains(unit))
            units.Add(unit);
    }

    private void OnTriggerExit(Collider other)
    {
        UnitHealth unit = other.transform.GetComponent<UnitHealth>();

        if (unit != null && units.Contains(unit))
            units.Remove(unit);
    }
    #endregion

    private void Update()
    {
        if (units.Count >= unitsCount && ! triggered)
            TriggerTrap();
    }
    private void TriggerTrap()
    {
        triggered = true;
        StartCoroutine(ColapseAllTrees());
        KillUnits();
    }

    private IEnumerator ColapseAllTrees()
    {
        yield return StartCoroutine(ColapseTree(trees[0], 1, 1));
        yield return StartCoroutine(ColapseTree(trees[1], 1, -1));
        yield return StartCoroutine(ColapseTree(trees[2], -1, -1));
        yield return StartCoroutine(ColapseTree(trees[3], -1, 1));
    }
    private void KillUnits()
    {
        for (int i = 0; i < units.Count; i++)
            units[i].Die();
    }
    private IEnumerator ColapseTree(GameObject tree, int a, int b)
    {
        float currentTime = 0f;

        while(currentTime < duration)
        {
            tree.transform.Rotate(angle * a, 0f, angle * b);    
        
            currentTime += Time.deltaTime;
            yield return null;
        }

        yield break;
    }

}
