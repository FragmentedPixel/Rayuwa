using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsManager : MonoBehaviour
{

    public void StartBattle()
    {
        UnitController[] controllers = FindObjectsOfType<UnitController>();

        foreach(UnitController controller in controllers)
            controller.StartBattle();
    }
}
