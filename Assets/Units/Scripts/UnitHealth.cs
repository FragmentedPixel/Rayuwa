using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHealth : MonoBehaviour {

	public float MaxHealth=500;
	private float currentHealth;
	public Image background;
	public Image healthImage;
	private Canvas canvas;
	// Use this for initialization
	void Start () 
	{
		canvas = background.GetComponentInParent<Canvas>();

		currentHealth = MaxHealth;	
	}
	
	// Update is called once per frame
	void Update () 
	{
		canvas.transform.LookAt (Camera.main.transform);
	}

	public void Hit(float damage)
	{
		currentHealth -= damage;
		healthImage.fillAmount = currentHealth / MaxHealth;
		if (healthImage.fillAmount < 0.2)
			healthImage.color = Color.red;
		if (currentHealth <= 0)
		{
			Destroy (transform.parent.gameObject);
		}
	}
}
