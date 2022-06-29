using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabViewController : MonoBehaviour
{
    [Header("Tab Colors")]
    public Color selectedTabColor;
    public Color unselectedTabColor;
    
    [Header("View Panels")]
    public GameObject gameView;
    public GameObject shopView;
    public GameObject optionsView;
    
    [Header("Tab Button Images")]
    public Image gameTabButtonImage;
    public Image shopTabButtonImage;
    public Image optionsTabButtonImage;

    private void Start()
    {
        onClick_GameTab();
    }

    public void onClick_GameTab()
    {
        ResetUI();
        gameView.SetActive(true);
        gameTabButtonImage.color = selectedTabColor;
    }

    public void onClick_ShopTab()
    {
        ResetUI();
        shopView.SetActive(true);
        shopTabButtonImage.color = selectedTabColor;
    }

    public void onClick_OptionsTab()
    {
        ResetUI();
        optionsView.SetActive(true);
        optionsTabButtonImage.color = selectedTabColor;
    }

    private void ResetUI()
    {
        gameView.SetActive(false);
        shopView.SetActive(false);
        optionsView.SetActive(false);

        gameTabButtonImage.color = unselectedTabColor;
        shopTabButtonImage.color = unselectedTabColor;
        optionsTabButtonImage.color = unselectedTabColor;
    }
}
