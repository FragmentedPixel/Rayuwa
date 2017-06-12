using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSCamera : MonoBehaviour
{
    public float speed = 5f;
    [Range(0f, 1f)]
    public float edgePercent;

    private void Update()
    {
        float vertical = 0;
        float orizontal = 0;
        float scroll = 0;

        if (Input.GetKey(KeyCode.W))
            vertical = 1f;
        else if (Input.GetKey(KeyCode.S))
            vertical = -1f;

        if (Input.mousePosition.y < Screen.height * edgePercent)
            vertical = -1f;
        else if (Input.mousePosition.y > Screen.height * (1 - edgePercent))
            vertical = 1f;

        if (Input.GetKey(KeyCode.A))
            orizontal = -1f;
        else if (Input.GetKey(KeyCode.D))
            orizontal = 1f;

        if (Input.mousePosition.x < Screen.width * edgePercent)
            orizontal = -1f;
        else if (Input.mousePosition.x > Screen.width * (1 - edgePercent))
            orizontal = 1f;

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            scroll = -1;
        else if(Input.GetAxis("Mouse ScrollWheel") > 0)
            scroll = 1;

        transform.localPosition += new Vector3(orizontal, vertical, 0f).normalized * speed * Time.deltaTime;
        GetComponent<Camera>().orthographicSize -= scroll;
    }

}
