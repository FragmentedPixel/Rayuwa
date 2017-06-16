using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region Variabiles
    public AudioClip buttonSound;
    private AudioSource audioS;
    #endregion

    #region Initialization
    private void Start()
    {
        audioS = GetComponent<AudioSource>();
        audioS.volume = PlayerPrefsManager.GetMasterVolume();
    }
    #endregion

    #region Changing Scene
    public void ChangeScene(string scene)
    {
        Time.timeScale = 1f;
        audioS.PlayOneShot(buttonSound);
        StartCoroutine(ChangeSceneCR(scene));
    }

    private IEnumerator ChangeSceneCR(string scene)
    {
        yield return new WaitForSeconds(buttonSound.length);
        SceneManager.LoadScene(scene);
        yield break;
    }
    #endregion

    #region Methods
    public void Exit()
    {
        Application.Quit();
    }

    public void LoadLastLevel()
    {
        ProgressData progressData = FindObjectOfType<ProgressData>();
        SceneManager.LoadScene(progressData.LastLevel());
    }

    public void ReloadScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    public void LoadNextLevel()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        index++;
        SceneManager.LoadScene(index);
    }
    #endregion
}
