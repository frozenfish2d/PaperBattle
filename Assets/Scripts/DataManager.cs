using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class DataManager : MonoBehaviour
{
    [SerializeField]
    GameObject noInternet;
    private string id;

    const string MASTER_VOLUME_KEY = "master_volume";
    const string SFX_VOLUME_KEY = "sfx_volume";


    public string[] playerData = new string[8];

    public int score;
    public int stars;
    public int max_health;
    public int cur_health;

    private void Awake()
    {
        SetUpSingletone();
        id = SystemInfo.deviceUniqueIdentifier.Substring(0, 19);
        StartCoroutine(FirstLoadData());
    }

    private void SetUpSingletone()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    IEnumerator  FirstLoadData()
    {
        WWW www = new WWW("http://dailysheet.ru/paper/loaddata.php?id="+id);
        yield return www;
        if ((www != null) && (www.text.Length > 1))
        {
            playerData = www.text.Split('\t');
            max_health = 1000 + System.Convert.ToInt32(playerData[1]) * 100;
            cur_health = max_health;
            stars = System.Convert.ToInt32(playerData[7]);
        }
        else
        {
            Time.timeScale = 0;
            noInternet.SetActive(true);
        }

    }

    IEnumerator LoadData()
    {
        WWW www = new WWW("http://dailysheet.ru/paper/loaddata.php?id=" + id);
        yield return www;
        if ((www != null) && (www.text.Length > 1))
        {
            playerData = www.text.Split('\t');
            max_health = 1000 + System.Convert.ToInt32(playerData[1]) * 100;
            stars = System.Convert.ToInt32(playerData[7]);
        }
    }




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

    public void ReLoadAllData()
    {
        StartCoroutine(LoadData());
    }
    //-------------------------------------------------------------
    public void UpdateUnlockLevel(int level)
    {
        StartCoroutine(UnlockLevel(level));
    }
    IEnumerator UnlockLevel(int lvl)
    {
        WWW www = new WWW("http://dailysheet.ru/paper/unlocklvl.php?level="+lvl+"&id="+ playerData[0]);
        yield return www;
    }
    //-------------------------------------------------------------
    public void UpdateStars()
    {
        StartCoroutine(UpdStars());
    }
    IEnumerator UpdStars()
    {
        WWW www = new WWW("http://dailysheet.ru/paper/updatestars.php?stars=" + stars + "&id=" + playerData[0]);
        yield return www;
    }
    //-------------------------------------------------------------
    public void UpdateHiScore()
    {
        StartCoroutine(UpdHiScore());
    }
    IEnumerator UpdHiScore()
    {
        WWW www = new WWW("http://dailysheet.ru/paper/updatehiscore.php?hiscore=" + GetScore() + "&id=" + playerData[0]);
        yield return www;
    }
    //-------------------------------------------------------------
    public void UpdateHealth(int healthUpd)
    {
        StartCoroutine(UpdHealth(healthUpd));
    }
    IEnumerator UpdHealth(int value)
    {
        WWW www = new WWW("http://dailysheet.ru/paper/updatehealth.php?health=" + value + "&id=" + playerData[0]);
        yield return www;
    }
    //-------------------------------------------------------------
    public void UpdateDamage(int dmgUpd)
    {
        StartCoroutine(UpdDamage(dmgUpd));
    }
    IEnumerator UpdDamage(int value)
    {
        
        WWW www = new WWW("http://dailysheet.ru/paper/updatedamage.php?dmg=" + value + "&id=" + playerData[0]);
        yield return www;
    }
    //-------------------------------------------------------------
    public void UpdateFirespeed(int fireUpd)
    {
        StartCoroutine(UpdFire(fireUpd));
    }
    IEnumerator UpdFire(int value)
    {
        
        WWW www = new WWW("http://dailysheet.ru/paper/updatefirespeed.php?fire=" + value + "&id=" + playerData[0]);
        yield return www;
    }
    //-------------------------------------------------------------
    public void RewardStars(int stars)
    {
        StartCoroutine(RwrdStars(stars));
    }
    IEnumerator RwrdStars(int value)
    {
        int tmp = stars + value;
        WWW www = new WWW("http://dailysheet.ru/paper/updatestars.php?stars=" + tmp + "&id=" + playerData[0]);
        yield return www;
    }
    //-------------------------------------------------------------

    public int GetScore()
    {
        return score;
    }
    public int GetHiScore()
    {
        return System.Convert.ToInt32(playerData[4]);
    }
    public int GetStars()
    {
        return stars;
    }
    public int GetMaxHealth()
    {
        return max_health;
    }
    public int GetCurHealth()
    {
        return cur_health;
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }

    public void AddToStars(int starValue)
    {
        stars += starValue;
    }

    public void ResetGame()
    {
        score = 0;
        StartCoroutine(FirstLoadData());
    }
     
}
