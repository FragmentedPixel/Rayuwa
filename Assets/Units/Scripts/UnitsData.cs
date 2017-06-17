using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class UnitsData : MonoBehaviour
{
    #region Singelton
    public static UnitsData instance;

    public int maxUnits;
    public List<Unit> unitsList;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnLevelLoaded;
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

    private void OnLevelLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        foreach(Unit unit in unitsList)
            unit.count = 0;
    }
    #endregion
}

[Serializable]
public class Unit
{
    public GameObject prefab;
    public bool unlocked;
    public int count;

    [TextArea]
    public string strongPoints;

    public Unit()
    {
        prefab = null;
        unlocked = false;
        count = 0;
    }

}
