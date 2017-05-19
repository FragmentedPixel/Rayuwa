using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitsData : MonoBehaviour
{
    public static UnitsData instance;

    public int maxUnits;
    public int unitTypes;
    
    public Unit[] units;

    private void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    public bool canAddUnits { get { return GetCurrentUnitsCount() < maxUnits; } }
    private int GetCurrentUnitsCount()
    {
        int value = 0;
        foreach (Unit unit in units)
            value += unit.count;

        return value;
    }
}

[Serializable]
public class Unit
{
    public GameObject prefab;
    public bool unlocked;
    public int count;
}
