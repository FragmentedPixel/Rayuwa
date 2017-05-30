using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitsManager : MonoBehaviour
{
    #region Variabiles
    public Canvas unitsCanvas;
    public Canvas playerCanvas;

    public Button startButton;
    public Transform spawnPointsParent;

	private List<Transform> spawnPoints;
    private Unit[] units;
    #endregion

    #region Initialization
    public void Awake()
    {
        unitsCanvas.enabled = true;
        StartCoroutine(WaitForUnitsCR());
    }

    private IEnumerator WaitForUnitsCR()
    {
        while (!Input.GetKeyDown(KeyCode.Space))
            yield return null;

        unitsCanvas.enabled = false;
        playerCanvas.enabled = true;

        StartLevel();
    }

    private void StartLevel()
    {
        if (UnitsData.instance)
            units = UnitsData.instance.units;

        Spawn();
        StartCoroutine(WaitForStart());
    }

    private IEnumerator WaitForStart()
    {
        while (!Input.GetKeyDown(KeyCode.Space))
            yield return null;

        StartBattle();
        yield break;
    }

    public void Spawn()
    {
        spawnPoints = new List<Transform>();

        foreach (Transform t in spawnPointsParent)
            spawnPoints.Add(t);

        SuffleSpawnPoints();
        int spawnIndex = 0;

        if (units == null)
            return;

        foreach (Unit unit in units)
        {
            for (int i = 0; i < unit.count; i++)
                Instantiate(unit.prefab, spawnPoints[spawnIndex++].position, Quaternion.identity, transform);
        }
     }

    private void SuffleSpawnPoints()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            Transform temp = spawnPoints[i];
            int randomIndex = Random.Range(i, spawnPoints.Count);
            spawnPoints[i] = spawnPoints[randomIndex];
            spawnPoints[randomIndex] = temp;
        }
    }
    #endregion

    #region Methods
    public void StartBattle()
    {
        StopAllCoroutines();
        UnitController[] controllers = FindObjectsOfType<UnitController>();
        startButton.gameObject.SetActive(false);

		foreach(UnitController controller in controllers)
          	 controller.StartBattle();
    }
    #endregion

    #region Loosing
    public Canvas loosCanvas;

    private void Update()
    {
        if (transform.childCount <= 0)
            LostGame();
    }

    private void LostGame()
    {
        loosCanvas.enabled = true;
    }
    #endregion
}
