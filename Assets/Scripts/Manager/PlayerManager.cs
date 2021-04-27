using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region singleton
    public static PlayerManager Instance = null;

    const string MONEY_PREFS = "money";

    void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        if(!PlayerPrefs.HasKey(MONEY_PREFS))
        {
            PlayerPrefs.SetInt(MONEY_PREFS, 0);
        }
    }
    #endregion

    public int Money {
        get { return PlayerPrefs.GetInt(MONEY_PREFS); }
        set 
        {
            PlayerPrefs.SetInt(MONEY_PREFS, value);
        }
    }

    public void FinishRun(int reward)
    {
        Money += reward;
    }
}
