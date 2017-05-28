using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ProgressData : MonoBehaviour
{
    public int levelCount;

    private int levelUnlocked;
    private string fileLocation;

    private void Start()
    {
        DontDestroyOnLoad(this);
        fileLocation = Application.persistentDataPath + "/progress.data";
        Load();
    }

    private void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(fileLocation, FileMode.Create, FileAccess.Write, FileShare.Write);

        Progress progress = new Progress(levelUnlocked);

        bf.Serialize(file, progress);
        file.Close();
    }

    private void Load()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(fileLocation, FileMode.Open, FileAccess.Read, FileShare.Read);

        Progress progress = bf.Deserialize(file) as Progress;

        levelUnlocked = progress.levelUnlocked;
    }

    private void OnDestroy()
    {
        Save();
    }
}

[Serializable]
public class Progress
{
    public int levelUnlocked;


    public Progress(int _levelUnlocked)
    {
        levelUnlocked = _levelUnlocked;
    }
}

