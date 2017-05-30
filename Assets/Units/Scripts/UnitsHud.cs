using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsHud : MonoBehaviour
{
    #region Variabiles
    public GameObject unitPannel;
    public GameObject unitImage;
    #endregion

    #region Initialization
    public void SetUpHud()
    {
        UnitController[] controllers = GetComponentsInChildren<UnitController>();
        foreach (UnitController controller in controllers)
        {
            UnitHealth health = controller.transform.GetComponentInChildren<UnitHealth>();
            UnitHudElement currentElement = Instantiate(unitImage, unitPannel.transform).GetComponent<UnitHudElement>();
            currentElement.SetHudElement(controller, health);
        }
    }
    #endregion
}
