using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameOverview : MonoBehaviour
{
    [Header("UI")]
    public Text name;
    public Text strongPoints;
    public Text weakPoints;
    public Text description;

    [Header("Parameters")]
    public float offset = 30f;
    private bool pressed;

    [Header("Enemmies")]
    public EnemyOverview[] enemies;


    private void Start()
    {
        StartCoroutine(ShowEnemiesCR());
    }

    private IEnumerator ShowEnemiesCR()
    {
        for(int i = 0; i < enemies.Length; i++)
        {
            EnemyOverview current = enemies[i];

            float distanceToTarget = Vector3.Distance(Camera.main.transform.position, current.lookPoint.position);

            while (distanceToTarget > offset)
            {
                distanceToTarget = Vector3.Distance(Camera.main.transform.position, current.lookPoint.position);
                Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, current.lookPoint.position, distanceToTarget * Time.deltaTime);

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
    }

    private void FillInformation(EnemyOverview currentEnemy)
    {
        name.text = currentEnemy.enemy.name;
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

}

[Serializable]
public class EnemyOverview
{
    public Transform enemy;
    public string strongPoints;
    public string weakPoints;
    public string description;

    public Transform lookPoint { get { return enemy.GetComponentInChildren<EnemyHealth>().transform; } }
}
