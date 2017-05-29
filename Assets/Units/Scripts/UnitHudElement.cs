using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHudElement : MonoBehaviour
{
    #region UI Elements
    public Text ammoText;
    public Image healthImage;

    private Animator anim;
    private UnitInfo unit;
    #endregion

    #region Initialization
    public void SetHudElement(UnitController _unitController, UnitHealth _unitHealth)
    {
        unit = new UnitInfo(_unitController, _unitHealth);
        anim = GetComponentInChildren<Animator>();
    }
    #endregion

    #region Update
    private void Update()
    {
        healthImage.color = (unit.health > .2f) ? Color.green : Color.red;
        healthImage.fillAmount = unit.health;

        ammoText.text = (unit.health <= 0f) ? "Dead" : unit.ammoText;
        if (unit.ammo <= 0f)
            anim.SetBool("Blinking", true);
        else
            anim.SetBool("Blinking", false);
    }

    #endregion
}

public class UnitInfo
{
    public UnitController unitController;
    public UnitHealth unitHealth;

    public UnitInfo(UnitController _unitController, UnitHealth _unitHealth)
    {
        unitController = _unitController;
        unitHealth = _unitHealth;
    }

    public float health { get { return unitHealth.GetHealthPercent(); } }
    public int ammo { get { return unitController.ammo; } }
    public string ammoText { get { return unitController.GetAmmoText(); } }
}