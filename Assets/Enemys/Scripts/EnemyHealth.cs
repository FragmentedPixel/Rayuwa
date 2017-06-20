using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    #region Variabiles
    #region Health
    public float MaxHealth=500;
    public int enemyValue = 10;

    private float currentHealth;
    private bool dead = false;
    private EnemyController controller;


    #endregion
    #region UI
    public Image background;
	public Image healthImage;
	private Canvas canvas;
    #endregion
    private GameManager gameManager;
    #endregion

    #region Initialization
    private void Start () 
	{
        gameManager = FindObjectOfType<GameManager>();
		canvas = background.GetComponentInParent<Canvas>();
		currentHealth = MaxHealth;
        controller = GetComponentInParent<EnemyController>();
	}
    #endregion

    #region Bilboard
    private void Update () 
	{
        if (canvas != null)
		    canvas.transform.LookAt (Camera.main.transform);
	}
    #endregion

    #region HIt
    public void Hit(float damage,Transform attacker)
	{
        if (attacker != null)
            attacker = attacker.GetComponentInChildren<UnitHealth>().transform;
        currentHealth -= damage;
		healthImage.fillAmount = currentHealth / MaxHealth;
        EnemyTower et;
        if (controller != null && controller.Ammo())
        {
            if (controller.target == null)
                controller.target = attacker;

            else if (!controller.target.CompareTag("Unit") || (Vector3.Distance(controller.target.position, transform.position) > Vector3.Distance(transform.position, attacker.position) + controller.targetTreshold))
                controller.target = attacker;

            if (controller.DistanceToTarget() < controller.attackRange)
                controller.currentState.ToAttackState();
            else
                controller.currentState.ToChaseState();

            List<Transform> enemyList = EnemyManager.instance.enemyList;

            for (int i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i] == null || enemyList[i] == controller.transform)
                {
                    enemyList.Remove(enemyList[i]);
                    i--;
                }
                else if (Vector3.Distance(controller.transform.position, enemyList[i].position) < controller.agroRange&&damage!=0)
                {
                    enemyList[i].GetComponentInChildren<EnemyHealth>().Hit(0, controller.target);
                }
            }
        }  
        else if(controller==null&&damage!=0&&(et=GetComponentInParent<EnemyTower>()))
        {
            List<Transform> enemyList = EnemyManager.instance.enemyList;

            for (int i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i] == null)
                {
                    enemyList.Remove(enemyList[i]);
                    i--;
                }
                else if (Vector3.Distance(et.transform.position, enemyList[i].position) < et.agroRange)
                {
                    enemyList[i].GetComponentInChildren<EnemyHealth>().Hit(0, attacker);
                }
            }
        }

        if (healthImage.fillAmount < 0.2)
			healthImage.color = Color.red;
        if (currentHealth <= 0)
        {
            if(gameManager!= null)
                gameManager.levelResources += enemyValue;

            Die();
        }
	}

    private void Die()
    {
        if (dead)
            return;

        dead = true;

        if (controller != null)
        {
            EnemyDeathTrap trap = GetComponent<EnemyDeathTrap>();

            if (trap != null)
                trap.SpawnSmallGolems();

            controller.anim.SetTrigger("DeathTrigger");
            Destroy(controller.gameObject, 3f);
            Destroy(canvas.gameObject);

            Destroy(controller);
            Destroy(controller.agent);
            Destroy(gameObject);
        }
        else
            Destroy(transform.parent.gameObject);
    }
    #endregion
}
