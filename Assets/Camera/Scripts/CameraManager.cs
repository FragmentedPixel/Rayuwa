using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    private Camera[] camera_list;
    private int index = 0;

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
        float zPos = camera_list[index].transform.position.z;
        if (!(++index<camera_list.Length))
        {
            index = 0;
        }
        camera_list[index].gameObject.SetActive(true);

        Vector3 newPos = camera_list[index].transform.position;
        newPos.z = zPos;
        camera_list[index].transform.position = newPos;
    }

}
