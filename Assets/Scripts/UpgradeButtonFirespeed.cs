using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonFirespeed : MonoBehaviour
{


    public int updateCost;
    [SerializeField]
    Text buttonText;

    [SerializeField]
    Button button;

    DataManager dataManager;

    int cur_update;
    int start_price = 100;
    int max_update = 8;

    // Start is called before the first frame update
    void Start()
    {
        dataManager = FindObjectOfType<DataManager>();
        cur_update = System.Convert.ToInt32(dataManager.playerData[3]);
        updateCost = start_price + cur_update * 50;
    }

    // Update is called once per frame

    void Update()
    {
        if ((updateCost > dataManager.GetStars()) || (max_update <= cur_update))
        {
            button.interactable = false;
            buttonText.text = "MAX";
        }
        else {
            button.interactable = true;

        }
        updateCost = start_price + cur_update * 50;
        buttonText.text = updateCost.ToString();

    }

    public void MakeUpgrade()
    {
        cur_update++;
        dataManager.AddToStars(-updateCost);
        dataManager.UpdateStars();
        dataManager.UpdateFirespeed(cur_update);
    }
}

