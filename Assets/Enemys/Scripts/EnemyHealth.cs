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
	}
    #endregion

    #region Bilboard
    private void Update () 
	{
		canvas.transform.LookAt (Camera.main.transform);
	}
    #endregion

    #region HIt
    public void Hit(float damage)
	{
		currentHealth -= damage;
		healthImage.fillAmount = currentHealth / MaxHealth;

        if (healthImage.fillAmount < 0.2)
			healthImage.color = Color.red;
		if (currentHealth <= 0)
		    Destroy (transform.parent.gameObject);
	}
    #endregion
}
