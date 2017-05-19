using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsManager : MonoBehaviour
{
	public List<Transform> points;
    public Unit[] units;

    public void Start()
	{
        units = UnitsData.instance.units;
		Spawn ();
	}

    public void Spawn()
    {
        SuffleSpawnPoints();
        int spawnIndex = 0;

        foreach (Unit unit in units)
        {
            for (int i = 0; i < unit.count; i++)
                Instantiate(unit.prefab, points[spawnIndex++].position, Quaternion.identity, transform);
        }
     }

    private void SuffleSpawnPoints()
    {
        for (int i = 0; i < points.Count; i++)
        {
            Transform temp = points[i];
            int randomIndex = Random.Range(i, points.Count);
            points[i] = points[randomIndex];
            points[randomIndex] = temp;
        }
    }

    public void StartBattle()
    {
       UnitController[] controllers = FindObjectsOfType<UnitController>();

		foreach(UnitController controller in controllers)
          	 controller.StartBattle();
    }


}
