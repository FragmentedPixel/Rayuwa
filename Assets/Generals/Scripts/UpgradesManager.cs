using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class UpgradesManager : MonoBehaviour
{
    public int levelCount;
    public static UpgradesManager instance;
    public int[] upgradeArray;
    private string fileLocation;

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

        Upgrades progress = new Upgrades(upgradeArray);

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

        upgradeArray = upgrades.upgrade;
    }

    private void OnDestroy()
    {
        Save();
    }

    public void ApplyUpgrade(int index)
    {
        upgradeArray[index]++;
    }

    public int PresentUpgrades(int index)
    {
        return upgradeArray[index];
    }

}

[Serializable]
public class Upgrades
{
    public int[] upgrade;


    public Upgrades(int[] _upgrade1)
    {
        upgrade = _upgrade1;
    }
}

