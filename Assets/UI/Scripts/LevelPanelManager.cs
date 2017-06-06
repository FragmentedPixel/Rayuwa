using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPanelManager : MonoBehaviour
{
    public GameObject levelImage;

    private void Start()
    {
        PopulatePanel();
    }

    private void PopulatePanel()
    {
        int count = LevelsData.instance.levels.Length;
    }

}