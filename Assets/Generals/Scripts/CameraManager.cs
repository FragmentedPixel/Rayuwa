using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    private Camera[] camera_list;
    private int index = 0;
    // Update is called once per frame
    private void Start()
    {

        camera_list = transform.GetComponentsInChildren<Camera>(true);
    }

    void Update ()
    {
		if(Input.GetKeyDown(KeyCode.C))
        {
            ChangeCamera();
        }
	}

    void ChangeCamera()
    {
        camera_list[index].gameObject.SetActive(false);
        if (!(++index<camera_list.Length))
        {
            index = 0;
        }
        camera_list[index].gameObject.SetActive(true);

    }

}
