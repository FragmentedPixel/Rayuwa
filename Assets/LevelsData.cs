using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LevelsData : MonoBehaviour
{
    public static LevelsData instance;

    public bool[] levels;

    #region Serialization
    private string fileLocation;
    private void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);
        fileLocation = Application.persistentDataPath + "/levels.data";
        Load();
    }
    private void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(fileLocation, FileMode.Create, FileAccess.Write, FileShare.Write);

        Levels progress = new Levels(levels);

        bf.Serialize(file, progress);
        file.Close();
    }
    private void Load()
    {
        if (!File.Exists(fileLocation))
            return;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(fileLocation, FileMode.Open, FileAccess.Read, FileShare.Read);

        Levels progress = bf.Deserialize(file) as Levels;

        levels = progress.levels;
    }
    private void OnDestroy()
    {
        Save();
    }
    #endregion
}

[Serializable]
public class Levels
{
    public bool[] levels;

    public Levels(bool[] _levels)
    {
        levels = _levels;
    }
}
