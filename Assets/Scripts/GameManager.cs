using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public LargeNumber itemCount = new LargeNumber();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        itemCount = itemCount.StringToLargeNumber(PlayerPrefs.GetString("ItemCount", "0"));
        
        InvokeRepeating(nameof(SaveGame), 60, 60);
    }

    private void SaveGame()
    {
        //TODO: USE STRINGS.CS
        PlayerPrefs.SetString("ItemCount", itemCount.LargeNumberToString());
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
