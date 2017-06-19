using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class EnemiesOverview : MonoBehaviour
{
    [Header("UI")]
    public Text enemyName;
    public Text strongPoints;
    public Text weakPoints;
    public Text description;

    [Header("Parameters")]
    public float offset = 30f;
    
    [Header("Enemmies")]
    public EnemyOverview[] enemies;

    private bool pressed;
    private bool finished;
    private Vector3 cameraStartPosition;
    private Quaternion cameraStartRotation;

    private void Start()
    {
        if (enemies.Length == 0)
            finished = true;
        else
            StartCoroutine(ShowEnemiesCR());
    }
    private IEnumerator ShowEnemiesCR()
    {
        cameraStartPosition = Camera.main.transform.position;
        cameraStartRotation = Camera.main.transform.rotation;

        for(int i = 0; i < enemies.Length; i++)
        {
            EnemyOverview current = enemies[i];
            Vector3 targetPos = current.lookPoint.position;

            float distanceToTarget = Vector3.Distance(Camera.main.transform.position, targetPos);

            while (distanceToTarget > offset)
            {
                distanceToTarget = Vector3.Distance(Camera.main.transform.position, targetPos);
                Camera.main.transform.position = Vector3.Slerp(Camera.main.transform.position, targetPos, Time.deltaTime);

                LookAtEnemy(current);
                yield return null;
            }

            FillInformation(current);

            while (!pressed && !Input.GetKeyDown(KeyCode.Space))
            {
                LookAtEnemy(current);
                yield return null;
            }

            yield return null;
            pressed = false;
        }

        Camera.main.transform.position = cameraStartPosition;
        Camera.main.transform.rotation = cameraStartRotation;

        finished = true;
    }

    private void FillInformation(EnemyOverview currentEnemy)
    {
        enemyName.text = currentEnemy.enemy.name;
        strongPoints.text = currentEnemy.strongPoints;
        weakPoints.text = currentEnemy.weakPoints;
        description.text = currentEnemy.description;
    }
    private void LookAtEnemy(EnemyOverview currentEnemy)
    {
        Quaternion startRot = Camera.main.transform.rotation;
        Quaternion endRot = Quaternion.LookRotation(currentEnemy.lookPoint.position - Camera.main.transform.position);

        Camera.main.transform.rotation = Quaternion.Slerp(startRot, endRot, Time.deltaTime);
    }
    public void ShowNext()
    {
        pressed = true;
    }
    public bool isFinished { get { return finished; } }
}

[Serializable]
public class EnemyOverview
{
    public Transform enemy;
    public string strongPoints;
    public string weakPoints;
    [TextArea] public string description;

    public Transform lookPoint
    { get
        {
            EnemyHealth enemyhealth = enemy.GetComponentInChildren<EnemyHealth>();
            if (enemyhealth != null)
                return enemyhealth.transform;
            else
                return enemy.GetComponentInChildren<MeshRenderer>().transform;
        }
    }
}
