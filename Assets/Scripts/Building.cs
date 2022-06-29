using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Building", menuName = "ScriptableObjects/Building", order = 1)]
public class Building : ScriptableObject
{
    public string buildingName;
    public string buildingCost;
    public string buildingItemsPerSecond;
    public float buildingCostCurve = 1.0f;
}
