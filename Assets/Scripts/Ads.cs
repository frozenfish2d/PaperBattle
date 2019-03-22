using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class Ads : MonoBehaviour
{
#if UNITY_IOS
   private string gameId = "3078242";
#elif UNITY_ANDROID
    private string gameId = "3078243";
#endif

    [SerializeField]
    Button adButton;

    DataManager dataManager;
    LevelManager lvlManager;

    bool testMode = true;

    public string placementId = "rewardedVideo";


    void Start()
    {
        dataManager = FindObjectOfType<DataManager>();
        lvlManager = FindObjectOfType<LevelManager>();
        if (adButton)
        {
            adButton.onClick.AddListener(ShowAd);
        }

        if (Monetization.isSupported)
        {
            Monetization.Initialize(gameId, true);
        }
    }

    void Update()
    {
        if (adButton)
        {
            adButton.interactable = Monetization.IsReady(placementId);
        }
    }


    void ShowAd()
    {
        ShowAdCallbacks options = new ShowAdCallbacks();
        options.finishCallback = HandleShowResult;
        ShowAdPlacementContent ad = Monetization.GetPlacementContent(placementId) as ShowAdPlacementContent;
        ad.Show(options);
    }

    void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            dataManager.RewardStars(10);
            dataManager.ReLoadAllData();
            lvlManager.LoadStartMenu();


        }
        else if (result == ShowResult.Skipped)
        {
            Debug.LogWarning("The player skipped the video - DO NOT REWARD!");
        }
        else if (result == ShowResult.Failed)
        {
            Debug.LogError("Video failed to show");
        }
    }

}
