using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderSwitch : MonoBehaviour {

    public LeaderController leader;
    public UnitController unit;

	void Start ()
    {
        leader = GetComponentInChildren<LeaderController>(true);
        unit = GetComponentInChildren<UnitController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Switch(bool isLeader)
    {
        if(isLeader)
        {
            leader.transform.position = unit.transform.position;
            leader.transform.rotation = unit.transform.rotation;
            unit.agent.Stop();
            unit.agent.ClearPath();
        }
        else
        {
            unit.transform.position = leader.transform.position;
            unit.transform.rotation = leader.transform.rotation;
        }
        leader.gameObject.SetActive(isLeader);
        unit.gameObject.SetActive(!isLeader);
    }
}
