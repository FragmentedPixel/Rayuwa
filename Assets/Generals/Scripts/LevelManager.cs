using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public AudioClip buttonSound;
    private AudioSource audioS;

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
        audioS.volume = PlayerPrefsManager.GetMasterVolume();
    }

    public void ChangeScene(string scene)
    {
        audioS.PlayOneShot(buttonSound);
        StartCoroutine(ChangeSceneCR(scene));
    }

    private IEnumerator ChangeSceneCR(string scene)
    {
        yield return new WaitForSeconds(buttonSound.length);
        SceneManager.LoadScene(scene);
        yield break;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
