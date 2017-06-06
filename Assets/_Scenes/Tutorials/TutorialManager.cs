using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    public Transform unitsManager;
    public Canvas loseCanvas;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (unitsManager.childCount == 0)
            loseCanvas.enabled = true;
	}
}
