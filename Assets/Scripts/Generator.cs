using System;
using System.Collections;
using System.Collections.Generic;
using ModernProgramming;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public bool runGenerator = true;

    private LargeNumber itemsPerSecond = new LargeNumber();

    private void Start()
    {
        CalculateGeneration();
    }

    public void CalculateGeneration()
    {
        StopAllCoroutines();
        
        for (int i = 0; i < GameManager.instance.buildings.Length; i++)
        {
            LargeNumber buildingsOwned = new LargeNumber();
            buildingsOwned = buildingsOwned.StringToLargeNumber(PlayerPrefs.GetInt(GameManager.instance.buildings[i].buildingName, 0).ToString());

            LargeNumber buildingItemsPerSecond = new LargeNumber();
            buildingItemsPerSecond = buildingItemsPerSecond.StringToLargeNumber(GameManager.instance.buildings[i].buildingItemsPerSecond);

            LargeNumber result = new LargeNumber();
            result = result.MultiplyLargeNumber(buildingsOwned, buildingItemsPerSecond);
            
            itemsPerSecond.AddLargeNumber(result);
        }
        
        StartCoroutine(GenerateItems());
    }

    private IEnumerator GenerateItems()
    {
        while (runGenerator)
        {
            GameManager.instance.items.AddLargeNumber(itemsPerSecond);
            yield return new WaitForSeconds(1);
        }
    }
}
