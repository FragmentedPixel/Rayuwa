using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial4 : MonoBehaviour
{
    #region Variabiles
    public Text tutorialText;

    private bool selectedUnits;
    #endregion

    #region Tutorial Core
    private IEnumerator TutorialCR()
    {
        WaitForSeconds waitTime = new WaitForSeconds(.1f);

        yield return StartCoroutine(IntroCR());
        yield return waitTime;
        yield return StartCoroutine(HUDCR());
        yield return waitTime;
        yield return StartCoroutine(SelectHUDCR());
        yield return waitTime;
        yield return StartCoroutine(AmmoBlinkCR());
        yield return waitTime;

        tutorialText.text = "Good luck and pls give us 3 weeks intership.";
    }
    #endregion

    #region Tutorial Coroutines
    private IEnumerator IntroCR()
    {
        tutorialText.text = "Before the battle starts you can choose your units. You have a maxium number of units you can select, but you can ciorba them as you wish.";

        while (!selectedUnits)
            yield return null;

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
    #endregion

    #region Methods
    public void SelectedUnits()
    {
        selectedUnits = true;
    }
    #endregion
}
