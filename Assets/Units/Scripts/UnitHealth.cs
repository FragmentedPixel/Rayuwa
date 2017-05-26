using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHealth : MonoBehaviour
{
    #region Variabiles
    #region Health
    public float MaxHealth=500;
	private float currentHealth;
    private UnitController controller;
    #endregion

    #region UI
    public Image background;
	public Image healthImage;
	private Canvas healthCanvas;
    #endregion
    #endregion

    #region Initialization
    private void Start () 
	{
        controller = GetComponentInParent<UnitController>();
        healthCanvas = background.GetComponentInParent<Canvas>();
		currentHealth = MaxHealth;	
	}
    #endregion

    #region Bilboard
    private void Update () 
	{
		healthCanvas.transform.LookAt (Camera.main.transform);
	}
    #endregion

    #region Hit
    public void Hit(float damage, Transform attacker)
	{
		currentHealth -= damage;
		healthImage.fillAmount = currentHealth / MaxHealth;

        controller.HitByEnemy(attacker);

		if (healthImage.fillAmount < 0.2)
			healthImage.color = Color.red;

		if (currentHealth <= 0)
			Destroy (transform.parent.gameObject);
	}
    #endregion

    public float GetHealthPercent()
    {
        return (currentHealth / MaxHealth);
    }
}
