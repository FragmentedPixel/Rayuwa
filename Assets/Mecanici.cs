using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mecanici : MonoBehaviour {

    private Transform lastHit=null;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if(Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (lastHit != null)
                    lastHit.gameObject.SetActive(false);
                if (raycastHit.collider.tag=="Enemy")
                {
                    lastHit = raycastHit.transform.GetChild(0).transform;
                    lastHit.gameObject.SetActive(true);
                }
                
            }
            else
            {
                if(lastHit!=null)
                lastHit.gameObject.SetActive(false);
            }
        }
	}

}
