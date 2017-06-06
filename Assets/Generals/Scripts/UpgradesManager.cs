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
    private int levelResources;

    public int costPerLevel;
    private string fileLocation;

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

    public int UpgradeCost(int index)
    {
        return upgradeArray[index] * costPerLevel;
    }
    public bool ApplyUpgrade(int index)
    {
        int cost = UpgradeCost(index);

        if (cost > resources)
            return false;

        resources -= cost;
        upgradeArray[index]++;
        return true;
    }
    public int GetUpgradeValue(int index)
    {
        return upgradeArray[index];
    }
    public void Resources (int amount)
    {
        levelResources += amount;
        Debug.Log(levelResources);
    }
    public void ApplyResources(int bonus)
    {
        resources += levelResources+bonus;
        Debug.Log(levelResources + "Final");
        levelResources = 0;
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

