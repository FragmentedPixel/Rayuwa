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

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<UnitHealth>())
            Win();
    }

    private void Win()
    {
        EnemyHealth[] enemys = FindObjectsOfType<EnemyHealth>();
        if (enemys.Length != 0)
            return;
        if(FindObjectOfType<GameManager>()!=null)
            FindObjectOfType<GameManager>().WonGame();
        else
            winCanvas.enabled = true;
        Destroy(gameObject);

    }
    #endregion
}
