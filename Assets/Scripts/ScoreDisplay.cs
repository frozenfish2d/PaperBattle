using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    Text scoreText;
    DataManager dataManager;

    // Use this for initialization
    void Start () {
        scoreText = GetComponent<Text>();
        dataManager = FindObjectOfType<DataManager>();
        scoreText.text = dataManager.GetScore().ToString();
    }
	
	// Update is called once per frame
	void Update () {
        scoreText.text = dataManager.GetScore().ToString();

    }
}
