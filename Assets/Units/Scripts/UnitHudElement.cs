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
    public Image typeImage;
    public Text shortcutKey;

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
        shortcutKey.text = _unitController is MeleeUnitController ? "1" : "2";

        typeImage.sprite = _unitController.image;
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
        #region Health
        healthImage.color = (unit.health > .2f) ? Color.green : Color.red;
        healthImage.fillAmount = unit.health;
        #endregion

        #region Selecting
        Color newColor = buttonImage.color;

        if (drawing.selectedAgents.Contains(unit.unitController.agent))
            newColor.a = 1f;
        else
            newColor.a = .3f;

        buttonImage.color = newColor;
        #endregion
        
        #region Text
        if (unit.health <=0f)
        {
            Destroy(gameObject);
        }
        else if(unit.reloading)
        {
            ammoText.text = "Reloading";
            ammoText.color = Color.green;
            anim.SetBool("Blinking", false);
        }
        else if(unit.ammo <= 0f)
        {
            ammoText.text = unit.ammoText;
            anim.SetBool("Blinking", true);
        }
        else
        {
            ammoText.text = unit.ammoText;
            anim.SetBool("Blinking", false);
        }
        #endregion
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
    public bool reloading { get { return unitController.reloading; } }
    public string ammoText { get { return unitController.GetAmmoText(); } }
}