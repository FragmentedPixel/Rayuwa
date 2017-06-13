using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyTower : MonoBehaviour
{
    public GameObject[] enemiesToSpawn;
    public Transform wayPointsParent;
    public Image castBar;

    public float cooldown;
    private float currentTime;


    private void Update()
    {
        if (cooldown > currentTime)
            currentTime += Time.deltaTime;
        else
            SpawnEnemy();

        castBar.fillAmount = currentTime / cooldown;
    }

    private void SpawnEnemy()
    {
        int index = Random.Range(0, enemiesToSpawn.Length - 1);
        GameObject enemy = Instantiate(enemiesToSpawn[index], transform.position, transform.rotation, transform.parent);

        enemy.GetComponent<EnemyController>().wayPointsParent = wayPointsParent;

        currentTime = 0f;
    }

}
