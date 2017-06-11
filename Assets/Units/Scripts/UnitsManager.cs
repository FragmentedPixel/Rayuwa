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

    private List<UnitController> unitsControllers = new List<UnitController>();
    private List<Transform> spawnPoints;
    private Unit[] units;
    private Drawing drawing;
    #endregion

    #region Start Level
    public void StartLevel()
    {
        drawing = FindObjectOfType<Drawing>();
        if (UnitsData.instance)
            Spawn();
    }
    private void Spawn()
    {
        units = UnitsData.instance.units;

        SuffleSpawnPoints();
        PopulateUnitsControllers();
        SetUpDrawingAgents();
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

    private void SuffleSpawnPoints()
    {
        spawnPoints = new List<Transform>();

        foreach (Transform t in spawnPointsParent)
            spawnPoints.Add(t);


        for (int i = 0; i < spawnPoints.Count; i++)
        {
            Transform temp = spawnPoints[i];
            int randomIndex = Random.Range(i, spawnPoints.Count);
            spawnPoints[i] = spawnPoints[randomIndex];
            spawnPoints[randomIndex] = temp;
        }
    }
    private void SetUpDrawingAgents()
    {
        foreach (Transform t in transform)
            drawing.allAgents.Add(t.GetComponent<Agent>());

        foreach (Agent a in drawing.allAgents)
            drawing.selectedAgents.Add(a);
    }
    private void PopulateUnitsControllers()
    {
        int spawnIndex = 0;
        foreach (Unit unit in units)
        {
            for (int i = 0; i < unit.count; i++)
                unitsControllers.Add(Instantiate(unit.prefab, spawnPoints[spawnIndex++].position, Quaternion.identity, transform).GetComponent<UnitController>());
        }

    }
    #endregion
}
