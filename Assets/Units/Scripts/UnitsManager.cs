using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitsManager : MonoBehaviour
{
    #region Variabiles
    public Transform spawnPointsParent;
    public Button startButton;

    private List<Transform> spawnPoints;
    private Unit[] units;
    private Drawing drawing;
    #endregion

    #region Start Level
    public void StartLevel()
    {
        drawing = FindObjectOfType<Drawing>();

        if (UnitsData.instance)
            units = UnitsData.instance.units;

        Spawn();
        StartCoroutine(WaitForStart());
    }
    private void Spawn()
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

        foreach (Transform t in transform)
            drawing.allAgents.Add(t.GetComponent<Agent>());

        foreach (Agent a in drawing.allAgents)
            drawing.selectedAgents.Add(a);
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

    #region Start Battle
    private IEnumerator WaitForStart()
    {
        while (!Input.GetKeyDown(KeyCode.Space))
            yield return null;

        StartBattle();
        yield break;
    }
    public void StartBattle()
    {
        StopAllCoroutines();
        UnitController[] controllers = FindObjectsOfType<UnitController>();
        startButton.gameObject.SetActive(false);

        foreach (UnitController controller in controllers)
            controller.StartBattle();
    }
    #endregion
}
