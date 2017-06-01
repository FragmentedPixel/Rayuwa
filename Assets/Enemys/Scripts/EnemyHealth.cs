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
    #endregion
    #region UI
    public Image background;
	public Image healthImage;
	private Canvas canvas;
    #endregion
    #endregion

    #region Initialization
    private void Start () 
	{	
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
		currentHealth -= damage;
		healthImage.fillAmount = currentHealth / MaxHealth;

        if (controller.target.tag != "Units")
        {
            controller.target = attacker;
            controller.currentState.ToChaseState();
        }
        if (healthImage.fillAmount < 0.2)
			healthImage.color = Color.red;
		if (currentHealth <= 0)
		    Destroy (transform.parent.gameObject);
	}
    #endregion
}
