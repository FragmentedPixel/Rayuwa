using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentMusic : MonoBehaviour
{
    #region Varuabiles
    public AudioClip backgroundLoop;

    private AudioSource audioS;
    #endregion

    #region Initialization
    private void Start ()
    {
        StartMusic();
        DontDestroyOnLoad(this);
	}


    private void StartMusic()
    {
        audioS = GetComponent<AudioSource>();
        audioS.clip = backgroundLoop;
        audioS.volume = PlayerPrefsManager.GetMasterVolume();
        audioS.Play();
    }
    #endregion

    private void Update()
    {
        if(Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.O))
        {
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            buildIndex++;
            SceneManager.LoadScene(buildIndex);
        }
    }
}
