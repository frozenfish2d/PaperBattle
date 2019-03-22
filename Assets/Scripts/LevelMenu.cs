using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour {

    [SerializeField]
    Button[] levelButtons;
    DataManager dataManager;


    Color activeColor =  new Vector4 (1, 1, 1, 1);

    private void Awake()
    {
        dataManager = FindObjectOfType<DataManager>();
    }

    void Start () {
        for (int i = 0; i <= Convert.ToInt32(dataManager.playerData[5])-1; i++)
        {

                levelButtons[i].image.color = activeColor;
                levelButtons[i].interactable = true;
        }
    }
	

}
