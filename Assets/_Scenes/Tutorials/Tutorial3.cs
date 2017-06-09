using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial3 : MonoBehaviour
{
    #region Variabiles
    public Text tutorialText;
    public Button battleButton;

    private bool battleStarted;
    private Drawing drawing;

    public Transform enemyManager;
    public Transform gates;
    #endregion

    #region Initialization
    private void OnEnable()
    {
        drawing = FindObjectOfType<Drawing>();
        StartCoroutine(TutorialCR());

        FindObjectOfType<UnitsManager>().StartLevel();
        FindObjectOfType<UnitsHud>().SetUpHud();
        battleButton.gameObject.SetActive(false);

        FindObjectOfType<UnitsManager>().UpdateControllersList();
    }
    #endregion

    #region Tutorial Core
    private IEnumerator TutorialCR()
    {
        WaitForSeconds waitTime = new WaitForSeconds(.1f);

        yield return StartCoroutine(IntroCR());
        yield return waitTime;
        yield return StartCoroutine(SendToReloadPointCR());
        yield return waitTime;
        yield return StartCoroutine(StartBattleCR());
        yield return waitTime;
        yield return StartCoroutine(WaitToReachReloadCR());
        yield return waitTime;
        yield return StartCoroutine(TargetEnemyCR());
        yield return waitTime;
        yield return StartCoroutine(WaitForEnemyKill());

        //Auto reload.
        tutorialText.text = "Now that you have killed all the enemyes go to the crystal";

    }
    #endregion

    #region Tutorial Coroutines
    private IEnumerator IntroCR()
    {
        tutorialText.text = "Your units have limited resources. Make sure to send them to the reload points from time to time or they will automatically go after ammo when they need.";

        while (!Input.GetMouseButton(0) && !Input.GetKey(KeyCode.Space))
            yield return null;
    }
    private IEnumerator SendToReloadPointCR()
    {
        tutorialText.text = "Sometimes your units will run out of ammo. The swords will loose their durability and the others will run out of tomahawks. Send them to a reload point.";
        bool selected = false;

        while (!selected)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform.GetComponent<ReloadPoint>() && Input.GetMouseButtonDown(1) && drawing.selectedAgents.Count > 0)
                selected = true;

            yield return null;
        }
    }
    private IEnumerator StartBattleCR()
    {
        tutorialText.text = "Press the battle button and start the fight.";
        battleButton.gameObject.SetActive(true);

        while (!battleStarted)
            yield return null;

    }
    private IEnumerator WaitToReachReloadCR()
    {
        tutorialText.text = "Go to a reload point";
        bool reached = false;

        Agent[] agents = FindObjectsOfType<Agent>();
        ReloadPoint[] reloadPoints = FindObjectsOfType<ReloadPoint>();

        while (!reached)
        {
            if (HasReachedReloadPoint(agents, reloadPoints))
                reached = true;

            yield return null;
        }

        foreach(Transform child in gates)
        {
            child.gameObject.SetActive(false);
        }
        FindObjectOfType<Grid>().ReCalculateGird();

    }
    private IEnumerator TargetEnemyCR()
    {
        tutorialText.text = "Select some units and right click the enemy to target it.";
        bool targeted = false;

        while (!targeted)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Enemy") && Input.GetMouseButtonDown(1) && drawing.selectedAgents.Count > 0)
                targeted = true;

            yield return null;
        }
        tutorialText.text = "Take good care of your units, they are your only hope to save Rayuwa.";
    }

    private IEnumerator WaitForEnemyKill()
    {
        
        bool killed = false;

        while (!killed)
        {
            if (enemyManager.childCount==2)
                killed = true;

            yield return null;
        }
    }
    
    #endregion

    #region Methods
    public void BattleStart()
    {
        battleStarted = true;
    }

    private bool HasReachedReloadPoint(Agent[] agents, ReloadPoint[] points)
    {
        for (int i = 0; i < agents.Length; i++)
        {       for (int j = 0; j < points.Length; j++)
            {
                if (agents[i] == null || points[j] == null)
                    continue;
                float distance = Vector3.Distance(agents[i].transform.position, points[j].transform.position);
                if (distance < 2f)
                    return true;
            }
        }

        return false;
    }
    #endregion
}
