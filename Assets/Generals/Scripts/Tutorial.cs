using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    #region Variabiles
    public Text tutorialText;
    public Button battleButton;

    private bool battleStarted;
    private Drawing drawing;
    #endregion

    #region Initialization
    private void OnEnable()
    {
        drawing = FindObjectOfType<Drawing>();
        StartCoroutine(TutorialCR());
    }
    #endregion

    #region Tutorial Core
    private IEnumerator TutorialCR()
    {
        WaitForSeconds waitTime = new WaitForSeconds(.1f);

        yield return StartCoroutine(IntroCR());
        yield return waitTime;
        yield return StartCoroutine(MoveCameraCR());
        yield return waitTime;
        yield return StartCoroutine(SelectCR());
        yield return waitTime;
        yield return StartCoroutine(ShiftSelectCR());
        yield return waitTime;
        yield return StartCoroutine(DragCR());
        yield return waitTime;
        yield return StartCoroutine(SetPathCR());
        yield return waitTime;
        yield return StartCoroutine(StartBattleCR());
        tutorialText.text = "Tutorial done. Good luck and have fun.";
        yield return new WaitForSeconds(3f);

        FindObjectOfType<LevelManager>().ChangeScene("Level1");
    }
    #endregion

    #region Tutorial Coroutines
    private IEnumerator IntroCR()
    {
        tutorialText.text = "Welcome to Rayuwa. Your forests are controlled by the magic inside the crystal. In order free the nature, you must reach it.";

        while (!Input.GetMouseButton(0) && !Input.GetKey(KeyCode.Space))
            yield return null;
    }

    private IEnumerator MoveCameraCR()
    {
        tutorialText.text = "Move the camera by pressing A or D. To Rotate it, move your cursor at the edges of the screen.";
        bool moved = false;

        while (!moved)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                moved = true;

            yield return null;
        }
    }

    private IEnumerator SelectCR()
    {
        tutorialText.text = "Select a unit by clicking on it.";
        bool selected = false;
        drawing.selectedAgents.Clear();

        while (!selected)
        {
            if (drawing.selectedAgents.Count > 0)
                selected = true;

            yield return null;
        }
    }

    private IEnumerator ShiftSelectCR()
    {
        tutorialText.text = "Select more units by Shift + click.";
        bool selected = false;

        while (!selected)
        {
            if (drawing.selectedAgents.Count > 1 && !drawing.isdragging && Input.GetKey(KeyCode.LeftShift))
                selected = true;

            yield return null;
        }
    }

    private IEnumerator DragCR()
    {
        tutorialText.text = "Hold the left mouse button to drag for a faster selection.";
        bool dragged = false;
        drawing.selectedAgents.Clear();

        while (!dragged || drawing.isdragging)
        {
            if (drawing.isdragging && !Input.GetKey(KeyCode.LeftShift))
                dragged = true;

            yield return null;
        }

    }

    private IEnumerator SetPathCR()
    {
        tutorialText.text = "Set a path for the selected units by right clicking on the ground or click an enemy to target it.";
        
        bool pathGiven = false;
        
        while(!pathGiven)
        {
            if (Input.GetMouseButtonDown(1) && drawing.selectedAgents.Count > 0)
                pathGiven = true;

            yield return null;
        }
    }

    private IEnumerator StartBattleCR()
    {
        tutorialText.text = "Start the battle by pressing the button.";
        battleButton.gameObject.SetActive(true);

        while(!battleStarted)
        {
            yield return null;
        }

    }
    #endregion

    #region Methods
    public void BattleStart()
    {
        battleStarted = true;
    }
    #endregion
}
