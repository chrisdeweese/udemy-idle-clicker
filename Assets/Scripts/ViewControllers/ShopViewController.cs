using System;
using System.Collections;
using System.Collections.Generic;
using ModernProgramming;
using UnityEngine;

public class ShopViewController : MonoBehaviour
{
    public GameObject buyBuildingButtonPrefab;
    public Transform scrollView;

    private List<BuildingButton> buildingButtons;

    private void Start()
    {
        buildingButtons = new List<BuildingButton>();
        
        for (int i = 0; i < GameManager.instance.buildings.Length; i++)
        {
            BuildingButton newBuildingButton = Instantiate(buyBuildingButtonPrefab, scrollView).GetComponent<BuildingButton>();
            
            newBuildingButton.building = GameManager.instance.buildings[i];
            newBuildingButton.nameLabel.text = GameManager.instance.buildings[i].buildingName;
            newBuildingButton.priceLabel.text = GameManager.instance.buildings[i].buildingCost;
            newBuildingButton.ownedLabel.text = "Owned: " + PlayerPrefs.GetInt(newBuildingButton.building.buildingName, 0);
            
            newBuildingButton.button.onClick.AddListener(delegate { BuyUpgrade(newBuildingButton.building); });
            
            buildingButtons.Add(newBuildingButton);
        }
    }

    private void BuyUpgrade(Building requestedBuilding)
    {
        LargeNumber requestedCost = new LargeNumber();
        requestedCost = requestedCost.StringToLargeNumber(requestedBuilding.buildingCost);

        if (requestedCost.IsLessThan(GameManager.instance.items))
        {
            GameManager.instance.items.SubtractLargeNumber(requestedCost);
            
            PlayerPrefs.SetInt(requestedBuilding.buildingName, PlayerPrefs.GetInt(requestedBuilding.buildingName, 0) + 1);
            
            FindObjectOfType<Generator>().CalculateGeneration();
        }
    }

    private void OnEnable()
    {   
        for (int i = 0; i < GameManager.instance.buildings.Length; i++)
        {
            if (buildingButtons != null)
            {
                LargeNumber buildCost = new LargeNumber();
                buildCost = buildCost.StringToLargeNumber(GameManager.instance.buildings[i].buildingCost);
                
                buildingButtons[i].button.interactable = buildCost.IsLessThan(GameManager.instance.items);
            }
        }
    }
}
