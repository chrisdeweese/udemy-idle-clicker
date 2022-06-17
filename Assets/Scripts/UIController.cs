using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI itemCountLabel;

    private void Start()
    {
        itemCountLabel.text = PlayerPrefs.GetString("ItemCount", "0");
    }

    public void onClick_AddItem()
    {
        GameManager.instance.itemCount.AddOne();

        itemCountLabel.text = GameManager.instance.itemCount.LargeNumberToShortString();

        //TODO: SUBSCRIBE CLICK TO EVENT
    }
}
