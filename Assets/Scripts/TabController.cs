using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    public Color selectedTabColor;
    public Color unselectedTabColor;
    public GameObject gamePanel;
    public GameObject shopPanel;
    public GameObject optionsPanel;
    public Image gameButtonImage;
    public Image shopButtonImage;
    public Image optionsButtonImage;

    public void onClick_GameTab()
    {
        ClearUI();
        gamePanel.SetActive(true);
        gameButtonImage.color = selectedTabColor;
    }

    public void onClick_ShopTab()
    {
        ClearUI();
        shopPanel.SetActive(true);
        shopButtonImage.color = selectedTabColor;
    }

    public void onClick_OptionsTab()
    {
        ClearUI();
        optionsPanel.SetActive(true);
        optionsButtonImage.color = selectedTabColor;
    }
    
    private void ClearUI()
    {
        gamePanel.SetActive(false);
        shopPanel.SetActive(false);
        optionsPanel.SetActive(false);

        gameButtonImage.color = unselectedTabColor;
        shopButtonImage.color = unselectedTabColor;
        optionsButtonImage.color = unselectedTabColor;
    }
}
