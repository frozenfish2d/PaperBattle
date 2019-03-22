using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPrefsManager : MonoBehaviour
{

    const string MASTER_VOLUME_KEY = "master_volume";
    const string SFX_VOLUME_KEY = "sfx_volume";
    //const string DEFFICULTY_KEY = "difficulty";
    const string LEVEL_KEY = "level_unlocked_";
    const string POINTS_KEY = "points";

    public static void SetMasterVolume(float volume)
    {
        if (volume >= 0f && volume <= 1f)
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }
    }
    public static void SetSfxVolume(float volume)
    {
        if (volume >= 0f && volume <= 1f)
        {
            PlayerPrefs.SetFloat(SFX_VOLUME_KEY, volume);
        }
    }

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }

    public static float GetSfxVolume()
    {
        return PlayerPrefs.GetFloat(SFX_VOLUME_KEY);
    }

    public static void UnlockLevel(int level)
    {
        if (level <= SceneManager.sceneCountInBuildSettings - 1)
        {
            PlayerPrefs.SetInt(LEVEL_KEY + level.ToString(), 1);
        }
    }

    public static bool IsLevelUnlocked(int level)
    {
        int levelValue = PlayerPrefs.GetInt(LEVEL_KEY + level.ToString());
        if (level <= SceneManager.sceneCountInBuildSettings - 1)
        {
            if (PlayerPrefs.HasKey(LEVEL_KEY + level))
            {
                if (levelValue == 1)
                    return true;
            }
        }
        return false;
    }
/*
    public static void SetDifficulty(float diffuculty)
    {
        if (diffuculty >= 0 && diffuculty <= 3)
        {
            PlayerPrefs.SetFloat(DEFFICULTY_KEY, diffuculty);
        }
    }

    public static float GetDifficulty()
    {
        return PlayerPrefs.GetFloat(DEFFICULTY_KEY);
    } 
    */
}
