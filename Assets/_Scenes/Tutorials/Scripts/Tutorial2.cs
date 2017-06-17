using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial2 : MonoBehaviour
{
    #region Variabiles
    public Text tutorialText;

    private Drawing drawing;
    #endregion

    #region Initialization
    private void OnEnable()
    {
        drawing = FindObjectOfType<Drawing>();
        StartCoroutine(TutorialCR());

        FindObjectOfType<UnitsManager>().StartLevel();
        FindObjectOfType<UnitsHud>().SetUpHud();
        FindObjectOfType<UnitsManager>().UpdateControllersList();
    }
    #endregion

    #region Tutorial Core
    private IEnumerator TutorialCR()
    {
        WaitForSeconds waitTime = new WaitForSeconds(.1f);

        yield return StartCoroutine(IntroCR());
        yield return waitTime;
        yield return StartCoroutine(MouseOverEnemyCR());
        yield return waitTime;
        yield return StartCoroutine(SightAndAttack());
        yield return waitTime;
        yield return StartCoroutine(WaitForRangeCR());
        yield return waitTime;
        yield return StartCoroutine(InRangeCR());
        yield return waitTime;
        tutorialText.text = "You killed the defender, now continue your way to the crystal.";
    }
    #endregion

    #region Tutorial Coroutines
    private IEnumerator IntroCR()
    {
        tutorialText.text = "The crystal is feeding of Rayuwa's nature. It can controll the enviroment in order to protect itself. Be carefull.";

        while (!Input.GetMouseButton(0) && !Input.GetKey(KeyCode.Space))
            yield return null;
    }
    private IEnumerator MouseOverEnemyCR()
    {
		tutorialText.text = "Mouse over the golem in order to see it's sight and attack range.";
        bool hovered = false;

        while (!hovered)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Enemy"))
                hovered = true;

            yield return null;
        }
    }
    private IEnumerator SightAndAttack()
    {
        tutorialText.text = "The outer circle shows you his sight range."+Environment.NewLine+ "The inner red circle shows you his attack range." + Environment.NewLine + "Select some units and right click the enemy to target it.";
        bool targeted = false;

        while (!targeted)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Enemy") && Input.GetMouseButtonDown(1) && drawing.selectedAgents.Count > 0)
                targeted = true;

            yield return null;
        }
    }
    private IEnumerator WaitForRangeCR()
    {
        tutorialText.text = string.Empty;
        EnemyController enemyController = FindObjectOfType<EnemyController>();
        bool spotted = false;

        while (!spotted)
        {
            if (enemyController.target.CompareTag("Unit"))
                spotted = true;

            yield return null;
        }
    }
    private IEnumerator InRangeCR()
    {
        tutorialText.text = "The enemy spotted you. He will now start attacking you.";
        EnemyController enemyController = FindObjectOfType<EnemyController>();
        bool killed = false;

        while (!killed)
        {
            if (enemyController == null)
                killed = true;

            yield return null;
        }

    }
    #endregion

}
