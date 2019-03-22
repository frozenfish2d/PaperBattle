using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsDisplay : MonoBehaviour {
    Text starsText;
    DataManager dataManager;

	// Use this for initialization
	void Start () {
        starsText = GetComponent<Text>();
        dataManager = FindObjectOfType<DataManager>();
        starsText.text = dataManager.GetStars().ToString();
    }
	
	// Update is called once per frame
	void Update () {
        starsText.text = dataManager.GetStars().ToString();
    }
}
