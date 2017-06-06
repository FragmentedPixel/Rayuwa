using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        for (int i = count - 1; i > 0; i--)
        {
            GameObject image = Instantiate(levelImage, transform);

            image.GetComponent<Button>().enabled = LevelsData.instance.levels[i];
            string level = "Level" + i.ToString();
            image.GetComponent<Button>().onClick.AddListener(delegate { FindObjectOfType<LevelManager>().ChangeScene(level); });

            image.GetComponentInChildren<Text>().text = i.ToString();
        }
    }

}