using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private bool paused = false;
    private Canvas pauseCanvas;

    private void Start()
    {
        pauseCanvas = GetComponent<Canvas>();

        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SwitchState();
    }

    public void SwitchState()
    {
        paused = !paused;
        pauseCanvas.enabled = paused;
        Time.timeScale = (paused) ? 0f : 1f;
    }
}
