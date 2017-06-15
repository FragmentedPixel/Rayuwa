using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    #region Variabiles
    #region Health
    public float MaxHealth=500;
	private float currentHealth;
    private EnemyController controller;

    public int enemyValue=10;

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
        }

        if (healthImage.fillAmount < 0.2)
			healthImage.color = Color.red;
        if (currentHealth <= 0)
        {
            if(gameManager!= null)
                gameManager.levelResources += enemyValue;

            Destroy(transform.parent.gameObject);
        }
	}
    #endregion
}
