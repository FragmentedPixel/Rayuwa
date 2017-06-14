using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHealth : MonoBehaviour
{
    #region Variabiles

    #region Health
    public float MaxHealth = 500;
    public float currentHealth;
    private UnitsManager unitsManager;
    private UnitController contrller;
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
        contrller = transform.GetComponentInParent<UnitController>();
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
    public void Hit(float damage, Transform attacker = null)
    {
        currentHealth -= damage;
        healthImage.fillAmount = currentHealth / MaxHealth;

        if(contrller != null)
            contrller.HitByEnemy(attacker);

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
        currentHealth = Mathf.Clamp(currentHealth, 0f, MaxHealth);
        healthImage.fillAmount = currentHealth / MaxHealth;
        healthImage.color = (healthImage.fillAmount < 0.2) ? Color.red : Color.green;
    }
    #endregion
}

