using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameViewController : MonoBehaviour
{
    public Text itemCountLabel;
    public Button clickerButton;

    private void Awake()
    {
        UpdateUI();
    }

    private void Update()
    {
        UpdateUI();
    }

    public void onClick_AddItem()
    {
        GameManager.instance.items.AddOne();
        
        UpdateUI();
    }

    private void UpdateUI()
    {
        itemCountLabel.text = GameManager.instance.items.LargeNumberToShortString();
    }
}
