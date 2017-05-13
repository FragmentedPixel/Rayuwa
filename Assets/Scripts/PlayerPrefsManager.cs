using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour
{

    const string MASTER_VOLUME_KEY = "master_volume";

    public static void SetMasterVolume(float volume)
    {
        PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
    }

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY,8f);
    }

}