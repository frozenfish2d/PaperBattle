using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    [SerializeField]
    Text starsText;

    DataManager dataManager;

    // Start is called before the first frame update
    void Start()
    {
        dataManager = FindObjectOfType<DataManager>();
    }

    // Update is called once per frame
    void Update()
    {
        starsText.text = "You have " + dataManager.GetStars().ToString() + " stars";
    }


}
