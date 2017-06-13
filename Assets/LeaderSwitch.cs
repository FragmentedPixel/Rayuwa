﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderSwitch : MonoBehaviour {

    public LeaderController leader;
    public UnitController unit;

    public Canvas leaderCanvas;

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

        leaderCanvas.enabled = isLeader;
        leader.gameObject.SetActive(isLeader);
        unit.gameObject.SetActive(!isLeader);
    }
}