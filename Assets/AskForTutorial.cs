using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AskForTutorial : MonoBehaviour
{
    private Canvas askForTutorialCanvas;

    private void Start()
    {
        if (LevelsData.instance.levels[0] == true)
            return;

        AskTutorial();
    }

    public void AskTutorial()
    {
        askForTutorialCanvas = GetComponent<Canvas>();
        askForTutorialCanvas.enabled = true;
    }

    public void NoTutorial()
    {
        LevelsData.instance.levels[0] = true;
        askForTutorialCanvas.enabled = false;
    }
}
