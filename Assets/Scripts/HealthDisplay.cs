using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {

    Text healthText;
    DataManager dataManager;

    // Use this for initialization
    void Start()
    {
        healthText = GetComponent<Text>();
        dataManager = FindObjectOfType<DataManager>();
        healthText.text = dataManager.GetCurHealth().ToString();
    }
    // Update is called once per frame
    void Update()
    {
        healthText.text = dataManager.GetCurHealth().ToString();
    }
}
