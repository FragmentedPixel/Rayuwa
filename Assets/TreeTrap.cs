using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTrap : MonoBehaviour
{
    public List<UnitHealth> units = new List<UnitHealth>();
    public GameObject[] trees;
    public Transform colapsePoint;

    private void Start()
    {
        StartCoroutine(ColapseTree(trees[0], 1 ,1));
        StartCoroutine(ColapseTree(trees[1], 1, -1));

    }

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

    private void Update()
    {
        if(units.Count >= 2)
        {


            foreach (UnitHealth unit in units)
                unit.Die();
        }
    }

    private IEnumerator ColapseTree(GameObject tree, int a, int b)
    {
        float currentTime = 0f;
        float duration = 2f;

        while(currentTime < duration)
        {
            tree.transform.Rotate(5f * a, 0f, 5f * b);    
        
            currentTime += Time.deltaTime;
            yield return null;
        }

        yield break;
    }

}
