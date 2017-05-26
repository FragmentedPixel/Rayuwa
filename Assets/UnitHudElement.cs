using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHudElement : MonoBehaviour
{
    public Text ammoText;
    public Image healthImage;

    private UnitInfo unit;

    public void SetHudElement(UnitController _unitController, UnitHealth _unitHealth)
    {
        unit = new UnitInfo(_unitController, _unitHealth);
    }

    private void Update()
    {
        healthImage.fillAmount = unit.health;
        ammoText.text = unit.ammo;
    }

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
    public string ammo { get { return unitController.GetAmmoText(); } }
}