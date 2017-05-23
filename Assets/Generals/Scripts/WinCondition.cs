using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCondition : MonoBehaviour
{
    #region Variabiles
    public Canvas winCanvas;
    #endregion

    #region Win check
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<UnitHealth>())
            Win();
    }

    private void Win()
    {
        winCanvas.enabled = true;

        EnemyHealth[] enemys = FindObjectsOfType<EnemyHealth>();
        foreach (EnemyHealth enemy in enemys)
            Destroy(enemy.transform.parent.gameObject);
    }
    #endregion
}
