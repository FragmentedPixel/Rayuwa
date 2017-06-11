using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EnemyTreeWizard : MonoBehaviour
{
    public float range;
    public float damage;
    public float castTime = 0f;
    public Image castBar;
    
    private float currentCastTime = 0f;
    private TreeLauncher treeLauncher;
    private List<GameObject> allTrees;
    private Grid grid;

    private void Start()
    {
        treeLauncher = GetComponent<TreeLauncher>();
        grid = FindObjectOfType<Grid>();
        allTrees = GameObject.FindGameObjectsWithTag("Tree").ToList();
    }

    private void Update()
    {
        if (currentCastTime < castTime)
            currentCastTime += Time.deltaTime;

        else
            Throw();

        UpdateCastBar();
    }

    private void Throw()
    {
        Transform closestTree = allTrees[0].transform;
        allTrees.Remove(allTrees[0]);
        Transform target = FindObjectOfType<UnitController>().transform;

        closestTree.gameObject.layer = 11;
        EnemyTree enemyTree = closestTree.gameObject.AddComponent<EnemyTree>();
        enemyTree.damage = damage;

        if(target != null && Vector3.Distance(transform.position, target.position) < range)
            treeLauncher.Launch(closestTree, target);

        currentCastTime = 0f;
        grid.ReCalculateGird();
    }

    private void UpdateCastBar()
    {
        castBar.fillAmount = currentCastTime / castTime;
    }
}
