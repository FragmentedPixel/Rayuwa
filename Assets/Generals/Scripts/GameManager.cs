using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Variabiles
    public Canvas unitsCanavas;
    public Canvas playerCanvas;
    public Canvas looseCanvas;

    private bool unitsSelected;
    #endregion

    #region Init
    private void Start()
    {
        StartCoroutine(GameLoopCR());
    }
    private IEnumerator GameLoopCR()
    {
        yield return SelectUnitsCR();
        yield return StartLevelCR();
        yield return GameCR();
    }
    #endregion

    #region Phases
    private IEnumerator SelectUnitsCR()
    {
        unitsCanavas.enabled = true;

        while (!Input.GetKeyDown(KeyCode.Space) && !unitsSelected)
            yield return null;

        unitsCanavas.enabled = false;
        yield break;
    }
    private IEnumerator StartLevelCR()
    {
        playerCanvas.enabled = true;

        FindObjectOfType<UnitsManager>().StartLevel();
        FindObjectOfType<UnitsHud>().SetUpHud();

        yield break;
    }
    private IEnumerator GameCR()
    {
        UnitsManager unitsManager = FindObjectOfType<UnitsManager>();

        while (unitsManager.transform.childCount > 0)
            yield return null;

        looseCanvas.enabled = true;
        yield break;
    }
    #endregion

    #region Methods
    public void SelectUnits()
    {
        unitsSelected = true;
    }
    #endregion
}
