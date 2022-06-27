using System.Collections;
using System.Collections.Generic;
using ModernProgramming;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public ShopUpgradeScriptableObject upgrade1;
    public ShopUpgradeScriptableObject upgrade2;
    public ShopUpgradeScriptableObject upgrade3;

    public void onClick_BuyUpgrade1()
    {
        LargeNumber currentUpgradeCost = new LargeNumber();
        currentUpgradeCost = currentUpgradeCost.StringToLargeNumber(upgrade1.upgradeCost);
        
        //if(upgrade1.upgradeCost)
    }

    public void onClick_BuyUpgrade2()
    {
        
    }

    public void onClick_BuyUpgrade3()
    {
        
    }
}
