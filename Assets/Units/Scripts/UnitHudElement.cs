using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHudElement : MonoBehaviour
{
    #region UI Elements
    public Text ammoText;
    public Image healthImage;
    public Button addButton;

    private Image buttonImage;
    private Animator anim;
    private UnitInfo unit;
    private Drawing drawing;
    #endregion

    #region Initialization
    public void SetHudElement(UnitController _unitController, UnitHealth _unitHealth)
    {
        unit = new UnitInfo(_unitController, _unitHealth);
        anim = GetComponentInChildren<Animator>();
        drawing = FindObjectOfType<Drawing>();
        buttonImage = addButton.GetComponent<Image>();

        addButton.onClick.AddListener(delegate { AddAgent(unit.unitController.agent); });
    }

    private void AddAgent(Agent agent)
    {
        if(unit.unitController != null)
            drawing.selectedAgents.Add(agent);
    }
    #endregion

    #region Update
    private void Update()
    {
        healthImage.color = (unit.health > .2f) ? Color.green : Color.red;
        healthImage.fillAmount = unit.health;

        Color newColor = buttonImage.color;

        if (drawing.selectedAgents.Contains(unit.unitController.agent))
            newColor.a = 1f;
        else
            newColor.a = .5f;

        buttonImage.color = newColor;

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