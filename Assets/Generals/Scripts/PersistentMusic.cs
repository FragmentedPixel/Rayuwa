using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentMusic : MonoBehaviour
{
    public AudioClip backgroundLoop;

    private AudioSource audioS;

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
}
