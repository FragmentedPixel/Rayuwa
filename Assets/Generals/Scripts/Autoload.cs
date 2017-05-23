using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Autoload : MonoBehaviour
{
    #region Variabiles
    public string sceneName;

    public AudioClip introSound;
    private AudioSource audioS;
    #endregion

    #region Loading Scene after sounds end
    private void Start()
    {
        PlayIntroSound();
        StartCoroutine(LoadSceneCR());
    }

    private void PlayIntroSound()
    {
        audioS = GetComponent<AudioSource>();
        audioS.volume = PlayerPrefsManager.GetMasterVolume();
        audioS.PlayOneShot(introSound);
    }

    private IEnumerator LoadSceneCR()
    {
        yield return new WaitForSeconds(introSound.length + .5f);
        SceneManager.LoadScene(sceneName);
    }
    #endregion
}
