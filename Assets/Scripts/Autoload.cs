using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Autoload : MonoBehaviour
{
    public string sceneName;

    public AudioClip introSound;
    private AudioSource audioS;

    private void Start()
    {
        PlayIntroSound();
        StartCoroutine(LoadSceneCR());
    }

    private void PlayIntroSound()
    {
        audioS = GetComponent<AudioSource>();
		audioS.volume = 0.5f;
        audioS.PlayOneShot(introSound);
    }

    private IEnumerator LoadSceneCR()
    {
        yield return new WaitForSeconds(introSound.length + .5f);
        SceneManager.LoadScene(sceneName);
    }
}
