using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager instance;

    public int[] upgradeArray;
    public int resources;

    public int costPerLevel;
    private string fileLocation;

    public int UpgradeCost(int i)
    {
        return upgradeArray[i] * costPerLevel;
    }

    #region Serialization
    private void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);
        fileLocation = Application.persistentDataPath + "/upgrades.data";
        Load(); 
    }
    private void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(fileLocation, FileMode.Create, FileAccess.Write, FileShare.Write);

        Upgrades progress = new Upgrades(upgradeArray, resources);

        bf.Serialize(file, progress);
        file.Close();
    }
    private void Load()
    {
        if (!File.Exists(fileLocation))
            return;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(fileLocation, FileMode.Open, FileAccess.Read, FileShare.Read);

        Upgrades upgrades = bf.Deserialize(file) as Upgrades;

        upgradeArray = upgrades.upgrades;
        resources = upgrades.resources;
    }
    private void OnDestroy()
    {
        Save();
    }
    #endregion

    public bool ApplyUpgrade(int index)
    {
        int cost = upgradeArray[index] * costPerLevel;

        if (cost > resources)
            return false;

        resources -= cost;
        upgradeArray[index]++;
        return true;
    }

    public int PresentUpgrades(int index)
    {
        return upgradeArray[index];
    }

}

[Serializable]
public class Upgrades
{
    public int[] upgrades;
    public int resources;


    public Upgrades(int[] _upgrades, int _resources)
    {
        upgrades = _upgrades;
        resources = _resources;
    }
}

