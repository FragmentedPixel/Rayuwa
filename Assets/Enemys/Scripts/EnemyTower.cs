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

    private bool triggered = false;
    private float currentTime;
    private ParticleSystem spawnParticules;


    private void Start()
    {
        spawnParticules = GetComponentInChildren<ParticleSystem>();
        spawnParticules.Stop();
    }

    private void Update()
    {
        if (!triggered)
            return;

        if (cooldown > currentTime)
            currentTime += Time.deltaTime;
        else
            SpawnEnemy();

        castBar.fillAmount = currentTime / cooldown;
    }

    private void SpawnEnemy()
    {
        spawnParticules.Clear();
        spawnParticules.Stop();
        int index = Random.Range(0, enemiesToSpawn.Length);
        GameObject enemy = Instantiate(enemiesToSpawn[index], spawnParticules.transform.position, transform.rotation, transform.parent);

        enemy.GetComponent<EnemyController>().wayPointsParent = wayPointsParent;

        currentTime = 0f;
        spawnParticules.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        UnitHealth unit = other.GetComponent<UnitHealth>();

        if (unit != null && !triggered)
        {
            spawnParticules.Play();
            triggered = true;
        }
    }

}
