using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EnemyTreeWizard : MonoBehaviour
{
    public float attackRange;
    public float damage;
    public float castTime = 0f;
    public Image castBar;
    
    private float currentCastTime = 0f;
    private Transform target;
    private TreeLauncher treeLauncher;
    private List<GameObject> allTrees;
    private Grid grid;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        treeLauncher = GetComponent<TreeLauncher>();
        grid = FindObjectOfType<Grid>();
        allTrees = GameObject.FindGameObjectsWithTag("Tree").ToList();

        SetUpRange();
    }

    private void Update()
    {
        if (target == null)
            FindNewTarget();

        if (currentCastTime < castTime)
            currentCastTime += Time.deltaTime;

        else
            Throw();

        UpdateCastBar();
        LookAtTarget();
    }

    private void Throw()
    {
        Transform closestTree = allTrees[0].transform;
        allTrees.Remove(allTrees[0]);

        closestTree.gameObject.layer = 11;
        EnemyTree enemyTree = closestTree.gameObject.AddComponent<EnemyTree>();
        enemyTree.damage = damage;

        if(target != null && Vector3.Distance(transform.position, target.position) < attackRange)
            treeLauncher.Launch(closestTree, target);

        anim.SetTrigger("ThrowTrigger");
        currentCastTime = 0f;
        grid.ReCalculateGird();
    }

    private void UpdateCastBar()
    {
        castBar.fillAmount = currentCastTime / castTime;
    }

    private void LookAtTarget()
    {
        if (target == null)
            return;

        Vector3 lookPoint = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.LookAt(lookPoint);
    }

    private void FindNewTarget()
    {
        UnitController controller = FindObjectOfType<UnitController>();
        if(controller != null)
            target = controller.transform;
    }

    private void SetUpRange()
    {
        EnemyHealth enemyHealth = GetComponentInChildren<EnemyHealth>();

        Transform range = enemyHealth.transform.GetChild(0);
        Transform sight = enemyHealth.transform.GetChild(1);

        range.transform.localScale = Vector3.one * attackRange;
        sight.transform.localScale = Vector3.one * GetComponent<SphereCollider>().radius;
    }
}
