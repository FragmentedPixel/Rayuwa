using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial1 : MonoBehaviour
{
    #region Variabiles
    public Text tutorialText;
    public Button battleButton;

    private bool battleStarted;
    private Drawing drawing;
    public CameraManager cm;
    private Grid grid;
    private bool candraw = false;
    #endregion

    #region Initialization

    private void Update()
    {
        if(!candraw)
        drawing.isdragging = false;
    }

    private void OnEnable()
    {
        cm.enabled = false;
        drawing = FindObjectOfType<Drawing>();
        StartCoroutine(TutorialCR());
        grid = drawing.GetComponentInParent<Grid>();
        FindObjectOfType<UnitsManager>().StartLevel();
        FindObjectOfType<UnitsHud>().SetUpHud();
        battleButton.gameObject.SetActive(false);
        foreach (Node node in grid.grid)
            node.Deactivate();
    }
    #endregion

    #region Tutorial Core
    private IEnumerator TutorialCR()
    {
        WaitForSeconds waitTime = new WaitForSeconds(.1f);

        yield return StartCoroutine(IntroCR());
        yield return waitTime;
        yield return StartCoroutine(MoveCamera1CR());
        yield return waitTime;
        yield return StartCoroutine(MoveCamera2CR());
        yield return waitTime;
        yield return StartCoroutine(ChangeCameraCR());
        yield return waitTime;
        yield return StartCoroutine(MoveCamera3CR());
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
        tutorialText.text = "Well done. Now go to the crystal.";
    }
    #endregion

    #region Tutorial Coroutines
    private IEnumerator IntroCR()
    {
        tutorialText.text = "Welcome to Rayuwa. Your forests are controlled by the magic inside the crystal. In order free the nature, you must reach it. Click to continue.";

        while (!Input.GetMouseButton(0) && !Input.GetKey(KeyCode.Space))
            yield return null;
    }
    private IEnumerator MoveCamera1CR()
    {
        tutorialText.text = "Move the camera by pressing A or D.";
        bool moved_a = false;
        bool moved_d = false;

        while (!(moved_a&&moved_d))
        {
            if (Input.GetKey(KeyCode.A))
                moved_a = true;
            if (Input.GetKey(KeyCode.D))
                moved_d = true;

            yield return null;
        }
    }

    private IEnumerator MoveCamera2CR()
    {
        tutorialText.text = "To Rotate it, move your cursor at the edges of the screen or by pressing Q or E.";
        bool rotated = false;
        float screenMoveSize;
        screenMoveSize = Screen.width * 0.1f;
        while (!rotated)
        {
            if (Input.mousePosition.x < screenMoveSize|| Input.mousePosition.x > Screen.width - screenMoveSize)
                rotated = true;

            yield return null;
        }

        cm.enabled = true;
    }

    private IEnumerator ChangeCameraCR()
    {
        tutorialText.text = "Change the camera by using C.";
        bool changed = false;

        while (!changed)
        {
            if (Input.GetKey(KeyCode.C))
                changed = true;

            yield return null;
        }
        cm.enabled = false;
    }
    private IEnumerator MoveCamera3CR()
    {
        tutorialText.text = "This camera can be controlled with W and S";

        bool moved_w = false;
        bool moved_s = false;

        while (!(moved_w && moved_s))
        {
            if (Input.GetKey(KeyCode.W))
                moved_w = true;
            if (Input.GetKey(KeyCode.S))
                moved_s = true;

            yield return null;
        }
        cm.enabled = true;
    }
    private IEnumerator SelectCR()
    {
        tutorialText.text = "Select a unit by clicking on it.";
        bool selected = false;
        drawing.selectedAgents.Clear();

        while (!selected)
        {
            if (drawing.selectedAgents.Count > 0 && !drawing.isdragging)
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
        candraw = true;
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Input.GetMouseButtonDown(1) && drawing.selectedAgents.Count > 0 && Physics.Raycast(ray,out hit) && LayerMask.NameToLayer("Walkable") == hit.transform.gameObject.layer)
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
