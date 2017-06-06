using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCondition : MonoBehaviour
{
    #region Variabiles
    public Canvas winCanvas;
    public int winBonus = 100;
    #endregion

    #region Win check
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<UnitHealth>())
            Win();
    }

    private void Win()
    {

        EnemyHealth[] enemys = FindObjectsOfType<EnemyHealth>();
        if (enemys.Length != 0)
            return;
        UpgradesManager.instance.ApplyResources(winBonus);
        winCanvas.enabled = true;

    }
    #endregion
}
