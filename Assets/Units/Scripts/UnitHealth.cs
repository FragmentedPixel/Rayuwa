using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHealth : MonoBehaviour
{
    #region Variabiles
    #region Health
    public float MaxHealth = 500;
    private float currentHealth;
    private UnitsManager unitsManager;
    #endregion

    #region UI
    public Image background;
    public Image healthImage;
    private Canvas healthCanvas;
    #endregion


    #endregion

    #region Initialization
    private void Start()
    {
        unitsManager = FindObjectOfType<UnitsManager>();
        healthCanvas = background.GetComponentInParent<Canvas>();
        currentHealth = MaxHealth;
    }
    #endregion

    #region Bilboard
    private void Update()
    {
        healthCanvas.transform.LookAt(Camera.main.transform);
    }
    #endregion

    #region Health Management
    public void Hit(float damage, Transform attacker)
    {
        currentHealth -= damage;
        healthImage.fillAmount = currentHealth / MaxHealth;

        unitsManager.UnitsInRange(transform.position, attacker);
        
        healthImage.color = (healthImage.fillAmount < 0.2) ? Color.red : Color.green;

        if (currentHealth <= 0)
            Destroy(transform.parent.gameObject);
    }
    public float GetHealthPercent()
    {
        return (currentHealth / MaxHealth);
    }
    public void Heal(float percent)
    {
        currentHealth += MaxHealth * percent;
        healthImage.fillAmount = currentHealth / MaxHealth;
        healthImage.color = (healthImage.fillAmount < 0.2) ? Color.red : Color.green;
    }
    public void Die()
    {
        Hit(MaxHealth, null);
    }
    #endregion
}

