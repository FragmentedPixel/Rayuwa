using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CinematicCamera : MonoBehaviour
{
    public CameraFrame[] frames;

    private void Start()
    {
        StartCoroutine(CameraMovementCR());
    }

    private IEnumerator CameraMovementCR()
    {
        foreach (CameraFrame frame in frames)
        {
            float currentTime = 0f;

            while (currentTime < frame.duration)
            {
                transform.position = Vector3.Lerp(frame.start.position, frame.final.position, currentTime / frame.duration);
                transform.LookAt(frame.target);

                currentTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}

[Serializable]
public class CameraFrame
{
    public Transform start;
    public Transform final;
    public Transform target;
    public float duration;
}