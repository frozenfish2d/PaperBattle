using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiScoreDisplay : MonoBehaviour
{
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text hiScoreText;
    [SerializeField]
    Text hiScoreInfo;
    DataManager dataManager;


    // Start is called before the first frame update
    void Start()
    {
        dataManager = FindObjectOfType<DataManager>();
        scoreText.text = dataManager.GetScore().ToString();
        hiScoreText.text = dataManager.GetHiScore().ToString();
        if (dataManager.GetScore()> dataManager.GetHiScore())
        {
            hiScoreInfo.text = "NEW HISCORE!!!";
            hiScoreText.text = dataManager.GetScore().ToString();
            dataManager.UpdateHiScore();
        }
    }

}
