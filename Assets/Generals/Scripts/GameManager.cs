using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Variabiles
    public int levelIndex;
    public int winBonus = 100;

    [Header("UI")]
    public Canvas unitsCanavas;
    public Canvas playerCanvas;
    public Canvas winCanvas;
    public Canvas looseCanvas;

    public float bonusPercent;
    public bool won;
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

        while (unitsManager.transform.childCount > 0 && !won)
        {
            Debug.Log(true && false);
            yield return null;
        }

        if (won)
            yield return Win();
        else
            yield return Loose();
    }

    private IEnumerator Win()
    {
        winCanvas.enabled = true;
        int reward = Mathf.RoundToInt(winBonus + winBonus * bonusPercent);
        UpgradesManager.instance.ApplyResources(reward);
        LevelsData.instance.levels[levelIndex+1] = true;

        yield return null;
    }
    private IEnumerator Loose()
    {
        looseCanvas.enabled = true;

        yield return null;
    }
    #endregion

    #region Methods
    public void SelectUnits()
    {
        unitsSelected = true;
    }

    public void WonGame()
    {
        won = true;
    }
    #endregion
}
