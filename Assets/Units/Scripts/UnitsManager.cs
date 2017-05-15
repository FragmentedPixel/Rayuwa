using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsManager : MonoBehaviour
{
	public int unitCount=5;
	public List<Transform> points;
	public GameObject units;

	public void Start()
	{
		Spawn ();
	}

    public void Spawn()
    {
		for (int i = 0; i < points.Count; i++)
		{
			Transform temp = points [i];
			int randomIndex = Random.Range (i, points.Count);
			points [i] = points [randomIndex];
			points [randomIndex] = temp;
		}
		for(int i =0;i<unitCount;i++)
		{
			Instantiate(units,points[i].position,Quaternion.identity,transform);
		}
     }

    public void StartBattle()
    {
       UnitController[] controllers = FindObjectsOfType<UnitController>();

		foreach(UnitController controller in controllers)
          	 controller.StartBattle();
    }


}
