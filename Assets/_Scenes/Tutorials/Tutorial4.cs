using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial4 : MonoBehaviour
{
    #region Variabiles
    public Text tutorialText;

    private bool selectedUnits;
    public CameraManager cm;
    #endregion

    #region Initialization
    private void Start()
    {
        cm.enabled = false;
        
        StartCoroutine(TutorialCR());
    }
    #endregion

    #region Tutorial Core
    private IEnumerator TutorialCR()
    {
        WaitForSeconds waitTime = new WaitForSeconds(.1f);

        yield return StartCoroutine(IntroCR());
        yield return waitTime;
        yield return StartCoroutine(MoveCamera1CR());
        yield return waitTime;
        yield return StartCoroutine(ChangeCameraCR());
        yield return waitTime;
        yield return StartCoroutine(MoveCamera3CR());
        yield return waitTime;
        yield return StartCoroutine(HUDCR());
        yield return waitTime;
        yield return StartCoroutine(SelectHUDCR());
        yield return waitTime;
        yield return StartCoroutine(AmmoBlinkCR());
        yield return waitTime;
        yield return StartCoroutine(GoodLuckCR());
        yield return waitTime;

        tutorialText.transform.GetComponentInParent<Canvas>().enabled = false;
    }
    #endregion

    #region Tutorial Coroutines
    private IEnumerator IntroCR()
    {
        tutorialText.text = "Before the battle starts you can choose your units. You have a maxium number of units you can select, but you can ciorba them as you wish.";

        while (!selectedUnits)
            yield return null;

    }

    private IEnumerator MoveCamera1CR()
    {
        tutorialText.text = "On large battlegrounds you will be able to move the camera. Move it by pressing A or D.";
        bool moved_a = false;
        bool moved_d = false;

        while (!(moved_a && moved_d))
        {
            if (Input.GetKey(KeyCode.A))
                moved_a = true;
            if (Input.GetKey(KeyCode.D))
                moved_d = true;

            yield return null;
        }
        cm.enabled = true;
    }

    private IEnumerator ChangeCameraCR()
    {
        tutorialText.text = "Change the camera by using C.";
        bool changed = false;

        while (!changed)
        {
            if (Input.GetKey(KeyCode.C))
                changed = true;

            yield return null;
        }
        cm.enabled = false;
    }
    private IEnumerator MoveCamera3CR()
    {
        tutorialText.text = "This camera can be controlled with W and S";

        bool moved_w = false;
        bool moved_s = false;

        while (!(moved_w && moved_s))
        {
            if (Input.GetKey(KeyCode.W))
                moved_w = true;
            if (Input.GetKey(KeyCode.S))
                moved_s = true;

            yield return null;
        }
        cm.enabled = true;
    }

    private IEnumerator HUDCR()
    {
        tutorialText.text = "On the bottom of your screen you can see your HUD. All your units are listed there. Below each one you can see their Health and Ammo.";

        while (!Input.GetMouseButton(0) && !Input.GetKey(KeyCode.Space))
            yield return null;
    }
    private IEnumerator SelectHUDCR()
    {
        tutorialText.text = "You can select units from your HUD, or shift select to add them to your currently selected units.";

        while (!Input.GetMouseButton(0) && !Input.GetKey(KeyCode.Space))
            yield return null;
    }
    private IEnumerator AmmoBlinkCR()
    {
        tutorialText.text = "When a unit is out of ammo, their ammo start blinking and they automatically go to reload.";

        while (!Input.GetMouseButton(0) && !Input.GetKey(KeyCode.Space))
            yield return null;
    }
    private IEnumerator GoodLuckCR()
    {
        tutorialText.text = "Good luck and pls give us 3 weeks intership.";

        while (!Input.GetMouseButton(0) && !Input.GetKey(KeyCode.Space))
            yield return null;
    }



    #endregion

    #region Methods
    public void SelectedUnits()
    {
        selectedUnits = true;
    }
    #endregion
}
