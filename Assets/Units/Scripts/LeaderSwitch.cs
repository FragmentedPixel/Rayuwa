using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderSwitch : MonoBehaviour {

    [HideInInspector] public LeaderController leader;
    [HideInInspector] public UnitController unit;

    public Canvas leaderCanvas;
    private bool isLeader;

	void Start ()
    {
        leader = GetComponentInChildren<LeaderController>(true);
        unit = GetComponentInChildren<UnitController>();
	}
	

	void Update ()
    {
		if (unit == null)
			Destroy (gameObject);

        if (!isLeader)
            leader.transform.position = unit.transform.position;
        else
            unit.transform.position = leader.transform.position;

    }

    public void Switch(bool isLeader)
    {
        this.isLeader = isLeader;
		leaderCanvas.enabled = isLeader;
		leader.gameObject.SetActive(isLeader);
		unit.gameObject.SetActive(!isLeader);
        if(isLeader)
        {
            leader.transform.position = unit.transform.position;
            leader.transform.rotation = unit.transform.rotation;
			leader.GetComponent<UnitHealth> ().currentHealth = unit.GetComponentInChildren<UnitHealth> ().currentHealth;
            unit.agent.Stop();
            unit.agent.ClearPath();
        }
        else
        {
            unit.transform.position = leader.transform.position;
            unit.transform.rotation = leader.transform.rotation;
			unit.GetComponentInChildren<UnitHealth> ().currentHealth = leader.GetComponent<UnitHealth> ().currentHealth;
        }
    }

	public void OnDestroy()
	{
		if(isLeader)
			FindObjectOfType<CameraManager> ().Leader ();
	}
}
