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
    private bool isLeader=false;
    [HideInInspector] public LeaderSwitch ls;

    private void Start()
    {
        //camera_list = transform.GetComponentsInChildren<Camera>(true);c
        camera_list = new Camera[2];
        camera_list[0] = scc.GetComponent<Camera>();
        camera_list[1] = ccc.GetComponent<Camera>();

        ls = FindObjectOfType<LeaderSwitch>();
    }

    private void Update ()
    {
		if(Input.GetKeyDown(KeyCode.C))
        {
            ChangeCamera();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            if(ls!=null)
                Leader();
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

        cakeslice.Outline[] outlines = FindObjectsOfType<cakeslice.Outline>();
        foreach (cakeslice.Outline outline in outlines)
            outline.enabled = false;
    }

    public void Leader()
    {
        isLeader = !isLeader;
        camera_list[index].gameObject.SetActive(!isLeader);
        ls.Switch(isLeader);
        //Cursor.visible = !isLeader;
        Cursor.lockState = isLeader ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
