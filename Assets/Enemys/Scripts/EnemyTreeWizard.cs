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

    public Transform bridge;
    
    private float currentCastTime = 0f;
    private Transform target;
    private TreeLauncher treeLauncher;
    private List<GameObject> allTrees;
    private Grid grid;
    private Animator anim;

    #region Initialization
    private void Start()
    {
        anim = GetComponent<Animator>();
        treeLauncher = GetComponent<TreeLauncher>();
        grid = FindObjectOfType<Grid>();
        allTrees = GameObject.FindGameObjectsWithTag("Tree").ToList();

        SetUpRange();
    }
    private void SetUpRange()
    {
        EnemyHealth enemyHealth = GetComponentInChildren<EnemyHealth>();

        Transform range = enemyHealth.transform.GetChild(0);
        Transform sight = enemyHealth.transform.GetChild(1);

        range.transform.localScale = Vector3.one * attackRange;
        sight.transform.localScale = Vector3.one * GetComponent<SphereCollider>().radius;
    }
    #endregion

    #region Updates
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
    #endregion

    #region Targeting + Throwing
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
    private void FindNewTarget()
    {
        UnitController controller = FindObjectOfType<UnitController>();
        if(controller != null)
            target = controller.transform;
    }
    #endregion

    #region Bridge Trigger
    private bool triggered;

    private void OnTriggerEnter(Collider other)
    {
        UnitHealth unit = other.GetComponent<UnitHealth>();

        if(unit != null && !triggered)
        {
            triggered = true;
            ThrowAtBridge();
        }
    }

    private void ThrowAtBridge()
    {
        if (bridge == null)
            return;

        Transform closestTree = allTrees[0].transform;
        allTrees.Remove(allTrees[0]);

        closestTree.gameObject.layer = 11;
        bridge.gameObject.layer = 10;
        EnemyTree enemyTree = closestTree.gameObject.AddComponent<EnemyTree>();
        enemyTree.damage = damage;

        
        treeLauncher.Launch(closestTree, bridge);

        anim.SetTrigger("ThrowTrigger");
        currentCastTime = 0f;
        grid.ReCalculateGird();
    }
    #endregion
}
