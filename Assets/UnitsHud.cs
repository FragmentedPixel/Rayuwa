using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsHud : MonoBehaviour
{
    public GameObject unitPannel;
    public GameObject unitImage;

    private void OnEnable()
    {
        UnitHudElement currentElement = Instantiate(unitImage, unitPannel.transform).GetComponent<UnitHudElement>();
        currentElement.SetHudElement(null, null);
    }

}
