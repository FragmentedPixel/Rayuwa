﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderController : MonoBehaviour {

    public float movementSpeed = 2f;
    public float cameraHorizontalSpeed = 2f;
    public float cameraVerticalSpeed = 1f;

    public UnitController unit;

    void Start ()
    {

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (unit.fightState.lastAttack + unit.fightSpeed < Time.time)
            {
                unit.fightState.lastAttack = Time.time;
                unit.target = FindObjectOfType<EnemyHealth>().transform;
                unit.FightTarget();
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        GetComponent<Rigidbody>().velocity =transform.TransformVector(new Vector3(horizontal, 0, vertical).normalized * Time.fixedDeltaTime * movementSpeed);

        //Rotates Player on "X" Axis Acording to Mouse Input
        float h = cameraHorizontalSpeed * Input.GetAxis("Mouse X");
        transform.Rotate(0, h, 0);

        //Rotates Player on "Y" Axis Acording to Mouse Input
        float v = cameraVerticalSpeed * Input.GetAxis("Mouse Y");
        Camera.main.transform.Rotate(-v, 0, 0);
        Vector3 rotation = Camera.main.transform.localRotation.eulerAngles;
        rotation.x = Mathf.Clamp(rotation.x, -30, 30);
        Camera.main.transform.localRotation = Quaternion.Euler(rotation);
    }
}