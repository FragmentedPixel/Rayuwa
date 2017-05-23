using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitsData : MonoBehaviour
{
    #region Singelton
    public static UnitsData instance;

    public int maxUnits;
    public List<Unit> unitsList;

    private void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }
    #endregion

    #region Properties 
    public Unit[] units{get { return unitsList.ToArray(); }    }
    public bool canAddUnits { get { return GetCurrentUnitsCount() < maxUnits; } }

    private int GetCurrentUnitsCount()
    {
        int value = 0;
        foreach (Unit unit in units)
            value += unit.count;

        return value;
    }
    #endregion
}

[Serializable]
public class Unit
{
    public Unit()
    {
        prefab = null;
        unlocked = false;
        count = 0;
    }

    public GameObject prefab;
    public bool unlocked;
    public int count;
}
