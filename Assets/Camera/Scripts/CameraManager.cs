using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    [Header("Offsets relative to last camera")]
    public float[] cameraOffsets;
    private Camera[] camera_list;
    public SideCameraController scc;
    public CenterCameraController ccc;
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

        index = (index + 1) % camera_list.Length;

        zPos = zPos+cameraOffsets[index];

        if (index == 0)
            scc.Clamp(zPos);
        else
            ccc.Clamp(zPos);

        camera_list[index].gameObject.SetActive(true);
    }

}
