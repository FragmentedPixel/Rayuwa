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
        for (int i = 1; i < count; i++)
        {
            GameObject image = Instantiate(levelImage, transform);

            image.GetComponent<Button>().enabled = LevelsData.instance.levels[i];
            string level = "Level" + i.ToString();
            image.GetComponent<Button>().onClick.AddListener(delegate { FindObjectOfType<LevelManager>().ChangeScene(level); });
            if (! LevelsData.instance.levels[i])
                image.GetComponent<Image>().color = Color.gray;

            image.GetComponentInChildren<Text>().text = i.ToString();
        }
    }
}