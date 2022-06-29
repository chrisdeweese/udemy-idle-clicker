using System;
using System.Collections;
using System.Collections.Generic;
using ModernProgramming;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public LargeNumber items = new LargeNumber();
    public Building[] buildings;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }
}
