﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    #region Variabiles
    [Header("UI Elements")]
    public Slider volumeSlider;
    public Dropdown qualityDropDown;
    public Dropdown resolutionsDropDown;
    public Toggle fullScreenCheckBox;
    public Slider scrollSlider;
    public Toggle inverseScrollCheckBox;

    [Header("Default Values")]
    public float defaultVolume = .8f;
    public bool defaultFullscreen;
    public float scrollValue = .15f;
    public bool defaultInverseScroll;
    #endregion

    #region Initialization
    private void Awake()
    {
        volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
        scrollSlider.value = PlayerPrefsManager.GetScrollBoundray();
        fullScreenCheckBox.isOn = PlayerPrefsManager.GetFullScreen();
        inverseScrollCheckBox.isOn = PlayerPrefsManager.GetInverseScroll();

        SetUpQuality();
        SetUpResolutions();
    }

    private void SetUpQuality()
    {
        string[] options = QualitySettings.names;
        for (var i = 0; i < options.Length; i++)
            qualityDropDown.options.Add(new Dropdown.OptionData() { text = options[i] });

        int level = PlayerPrefsManager.GetQualityLevel();
        qualityDropDown.value = level;
    }
    private void SetUpResolutions()
    {
        Resolution[] options = Screen.resolutions;
        for (var i = 0; i < options.Length; i++)
            resolutionsDropDown.options.Add(new Dropdown.OptionData() { text = options[i].width + "x" + options[i].height });

        int resolution = PlayerPrefsManager.GetResolution();
        resolutionsDropDown.value = resolution;
    }
    #endregion

    #region OnChange
    public void OnSoundChanged()
    {
        float value = volumeSlider.value;
        PlayerPrefsManager.SetMasterVolume(value);
        AudioSource[] sources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioS in sources)
            audioS.volume = value;
    }
    public void OnQualityChanged()
    {
        int newValue = qualityDropDown.value;
        PlayerPrefsManager.SetQualityLevel(newValue);
        QualitySettings.SetQualityLevel(newValue);
    }
    public void OnResolutionChanged()
    {
        int newRes = resolutionsDropDown.value;
        PlayerPrefsManager.SetResolution(newRes);
        
        Screen.SetResolution(Screen.resolutions[newRes].width, Screen.resolutions[newRes].height, Screen.fullScreen);
    }
    public void OnFullScreenChanged()
    {
        bool newFull = fullScreenCheckBox.isOn;
        PlayerPrefsManager.SetFullScreen(newFull);

        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, newFull);
    }
    public void OnScrollBoundrayChanged()
    {
        float value = scrollSlider.value;
        PlayerPrefsManager.SetScrollBoundray(value);
    }
    public void OnInverseScrollChanged()
    {
        bool newInverse = inverseScrollCheckBox.isOn;
        PlayerPrefsManager.SetInverseScroll(newInverse);
    }
    #endregion

    #region Default
    public void LoadDefault()
    {
        volumeSlider.value = defaultVolume;
        qualityDropDown.value = qualityDropDown.options.Count - 1;
        resolutionsDropDown.value = resolutionsDropDown.options.Count - 1;
        fullScreenCheckBox.isOn = defaultFullscreen;
    }
    #endregion

}
