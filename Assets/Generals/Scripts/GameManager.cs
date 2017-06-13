using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Variabiles
    [Header("Level")]
    public int levelIndex;
    public int winBonus = 100;

    [Header("UI")]
    public Canvas unitsCanavas;
    public Canvas playerCanvas;
    public Canvas winCanvas;
    public Canvas looseCanvas;
    public Canvas resourcesCanvas;

    [Header("Resources")]
    public int levelResources;
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
        yield return ResourcesCR();
    }
    #endregion

    #region Phases
    private IEnumerator SelectUnitsCR()
    {
        unitsCanavas.enabled = true;

        while (!unitsSelected)
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
            yield return null;

        if (won)
            yield return Win();
        else
            yield return Loose();
    }
    private IEnumerator ResourcesCR()
    {
		playerCanvas.enabled = false;
        resourcesCanvas.enabled = true;
        yield return null;
    }

    private IEnumerator Win()
    {
        winCanvas.enabled = true;

        int bonus = Mathf.RoundToInt((levelResources + winBonus) * bonusPercent / 100f);
        int totalRes = winBonus + levelResources + bonus;

        UpgradesManager.instance.ApplyResources(totalRes);
        LevelsData.instance.levels[levelIndex + 1] = true;

        FindObjectOfType<ResourcesPanel>().SetResources(levelResources, winBonus, bonus, totalRes);

        yield return null;
    }
    private IEnumerator Loose()
    {
        looseCanvas.enabled = true;

        int totalRes = levelResources;

        UpgradesManager.instance.ApplyResources(totalRes);

		FindObjectOfType<ResourcesPanel>().SetResources(levelResources, 0,  0, totalRes);

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
