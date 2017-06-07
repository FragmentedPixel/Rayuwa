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
        EnemyHealth[] enemys = FindObjectsOfType<EnemyHealth>();
        if (enemys.Length != 0)
            return;

        FindObjectOfType<GameManager>().WonGame();
    }
    #endregion
}
