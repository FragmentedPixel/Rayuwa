using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsHud : MonoBehaviour
{
    public GameObject unitPannel;
    public GameObject unitImage;

    private void OnEnable()
    {
        UnitController[] controllers = GetComponentsInChildren<UnitController>();
        foreach (UnitController controller in controllers)
        {
            UnitHealth health = controller.transform.GetComponentInChildren<UnitHealth>();
            UnitHudElement currentElement = Instantiate(unitImage, unitPannel.transform).GetComponent<UnitHudElement>();
            currentElement.SetHudElement(controller, health);
        }
    }

}
