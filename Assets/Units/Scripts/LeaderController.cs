using System.Collections;
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

    // Update is called once upon a frame
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

        Vector3 rotation = Camera.main.transform.localEulerAngles;
        if (rotation.x > 30&& rotation.x<200)
            rotation.x = 30;
        else if (rotation.x < 330&&rotation.x>200)
            rotation.x = 330;
        Camera.main.transform.localEulerAngles =rotation;
    }
}
