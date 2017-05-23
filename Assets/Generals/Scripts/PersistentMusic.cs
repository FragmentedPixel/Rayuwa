using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
