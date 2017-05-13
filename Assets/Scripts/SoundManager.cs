using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider volumeSlider;

    private void Start()
    {
        volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
    }

    private void Update()
    {
        ChangeSound(volumeSlider.value);
    }

    private void ChangeSound(float value)
    {
        volumeSlider.value = value;
        PlayerPrefsManager.SetMasterVolume(value);
        AudioSource[] sources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioS in sources)
            audioS.volume = value;
    }

    public void LoadDefault()
    {
        ChangeSound(.8f);
    }

}
