using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop Upgrade", menuName = "ScriptableObjects/Shop Upgrade", order = 1)]
public class ShopUpgradeScriptableObject : ScriptableObject
{
    public string upgradeName;
    public string upgradeCost;
    public float costCurve;
    public string itemsPerSecond;
}
