﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderController : MonoBehaviour {

    public float movementSpeed = 2f;
    public float cameraHorizontalSpeed = 2f;
    public float cameraVerticalSpeed = 1f;

    public UnitController unit;
    public Animator anim;
    private Rigidbody rb;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && (unit.fightState.lastAttack + unit.fightSpeed < Time.time))
        {
            unit.fightState.lastAttack = Time.time;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();

                if (enemy != null)
                    unit.target = enemy.transform;
            }

            if (unit.target != null)
            {
                //TODO: Event missing from animation.
                anim.SetTrigger("MeleeAttack");
                (unit as MeleeUnitController).SwordHit();
                
            }
        }
    }

    private void FixedUpdate ()
    {
        Movement();
        CharacterRotation();
        CameraRotation();
    }

    private void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        anim.SetBool("Walking", (horizontal + vertical != 0));

        rb.velocity = transform.TransformVector(new Vector3(horizontal, 0, vertical).normalized * Time.fixedDeltaTime * movementSpeed);
    }

    private void CameraRotation()
    {
        //Rotates Player on "Y" Axis Acording to Mouse Input
        float v = cameraVerticalSpeed * Input.GetAxis("Mouse Y");
        Camera.main.transform.Rotate(-v, 0, 0);

        Vector3 rotation = Camera.main.transform.localEulerAngles;
        if (rotation.x > 30 && rotation.x < 200)
            rotation.x = 30;
        else if (rotation.x < 330 && rotation.x > 200)
            rotation.x = 330;
        Camera.main.transform.localEulerAngles = rotation;
    }

    private void CharacterRotation()
    {

        //Rotates Player on "X" Axis Acording to Mouse Input
        float h = cameraHorizontalSpeed * Input.GetAxis("Mouse X");
        transform.Rotate(0, h, 0);
    }
}
