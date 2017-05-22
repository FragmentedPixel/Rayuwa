using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Text tutorialText;
    public Button battleButton;

    private bool battleStarted;
    private Drawing drawing;

    private void OnEnable()
    {
        drawing = FindObjectOfType<Drawing>();
        StartCoroutine(TutorialCR());
    }

    private IEnumerator TutorialCR()
    {
        yield return StartCoroutine(MoveCameraCR());
        yield return new WaitForSeconds(.1f);
        yield return StartCoroutine(SelectCR());
        yield return new WaitForSeconds(.1f);
        yield return StartCoroutine(ShiftSelectCR());
        yield return new WaitForSeconds(.1f);
        yield return StartCoroutine(DragCR());
        yield return new WaitForSeconds(.1f);
        yield return StartCoroutine(SetPathCR());
        yield return new WaitForSeconds(.1f);
        yield return StartCoroutine(StartBattleCR());
    }

    #region Tutorial Coroutines
    private IEnumerator MoveCameraCR()
    {
        tutorialText.text = "Move Camera";
        bool moved = false;

        while (!moved)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) )
                moved = true;

            yield return null;
        }
    }

    private IEnumerator SelectCR()
    {
        tutorialText.text = "Pls select units";
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
        tutorialText.text = "Pls shift sleect units";
        bool selected = false;
        drawing.selectedAgents.Clear();

        while (!selected)
        {
            if (drawing.selectedAgents.Count > 1)
                selected = true;

            yield return null;
        }
    }

    private IEnumerator DragCR()
    {
        tutorialText.text = "Pls drag";
        bool dragged = false;
        drawing.selectedAgents.Clear();

        while (!dragged || drawing.isdragging)
        {
            if (drawing.isdragging)
                dragged = true;

            yield return null;
        }

    }

    private IEnumerator SetPathCR()
    {
        tutorialText.text = "Pls path";
        bool pathGiven = false;
        
        while(!pathGiven)
        {
            if (Input.GetMouseButtonDown(1) && drawing.selectedAgents.Count > 0)
                pathGiven = true;

            yield return null;
        }

        tutorialText.text = "done path";
    }

    private IEnumerator StartBattleCR()
    {
        tutorialText.text = "Pls battle";
        battleButton.gameObject.SetActive(true);

        while(!battleStarted)
        {
            yield return null;
        }

        tutorialText.text = "Tutorial done";
    }
    #endregion

    #region Methods
    public void BattleStart()
    {
        battleStarted = true;
    }
    #endregion
}
