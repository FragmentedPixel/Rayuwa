using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitsManager : MonoBehaviour
{
    #region Variabiles

    public int maxUnits = 8;
    public float unitsRangeThreshold = 5f;
    public Transform spawnPointsParent;
    public Button startButton;

    private List<UnitController> unitsControllers = new List<UnitController>();
    private List<Transform> spawnPoints;
    private Unit[] units;
    private Drawing drawing;
    #endregion

    #region Start Level
    private void Awake()
    {
        UnitsData.instance.colorIndex = 0;
    }

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

        if (units != null)
        {
            foreach (Unit unit in units)
            {
                for (int i = 0; i < unit.count; i++)
                    unitsControllers.Add(Instantiate(unit.prefab, spawnPoints[spawnIndex++].position, Quaternion.identity, transform).GetComponent<UnitController>());
            }
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
        while (!Input.GetKeyDown(KeyCode.Space)||true)
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

    #region Methods
    public void UnitsInRange(Vector3 unitPosition, Transform newTarget)
    {

        for (int i = 0; i < unitsControllers.Count; i++)
        {
            if(unitsControllers[i] == null)
            {
                unitsControllers.Remove(unitsControllers[i]);
                i--;
            }
            else if (Vector3.Distance(unitsControllers[i].transform.position, unitPosition) < unitsRangeThreshold)
                unitsControllers[i].HitByEnemy(newTarget);
        }
    }

    public void UpdateControllersList()
    {
        UnitController[] controllers = FindObjectsOfType<UnitController>();
        unitsControllers = new List<UnitController>();

        foreach (UnitController controller in controllers)
            unitsControllers.Add(controller);
    }

    #endregion
}
