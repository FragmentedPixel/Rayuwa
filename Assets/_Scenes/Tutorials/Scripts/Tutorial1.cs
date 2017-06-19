using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial1 : MonoBehaviour
{
    #region Variabiles
    public Text tutorialText;
    public bool canMove=false;
    private Agent[] agents;
    private Drawing drawing;
    private Grid grid;
    private bool candraw = false;
    #endregion

    #region Initialization
    private void OnEnable()
    {
        LevelsData.instance.levels[0] = true;
        drawing = FindObjectOfType<Drawing>();
        

        grid = drawing.GetComponentInParent<Grid>();
        FindObjectOfType<UnitsManager>().StartLevel();
        FindObjectOfType<UnitsHud>().SetUpHud();
        agents = FindObjectsOfType<Agent>();
        foreach (Node node in grid.grid)
            node.Deactivate();

        FindObjectOfType<UnitsManager>().UpdateControllersList();
        StartCoroutine(TutorialCR());
    }

    private void Update()
    {
        if (!candraw)
            drawing.isdragging = false;
        if (!canMove)
            foreach (Agent agent in agents)
                agent.Stop();
    }
    #endregion

    #region Tutorial Core
    private IEnumerator TutorialCR()
    {
        WaitForSeconds waitTime = new WaitForSeconds(.1f);
        WaitForSeconds longTime = new WaitForSeconds(1f);

        yield return StartCoroutine(IntroCR());
        yield return waitTime;
        yield return StartCoroutine(SelectCR());
        yield return waitTime;
        yield return StartCoroutine(ShiftSelectCR());
        yield return longTime;
        yield return StartCoroutine(DragCR());
        yield return waitTime;
        yield return StartCoroutine(SetPathCR());
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
        tutorialText.text = "Select more units by Shift + left click.";
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
        canMove = true;
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
    #endregion
}
