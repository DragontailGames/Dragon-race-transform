using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region singleton
    public static PlayerManager Instance = null;

    const string POINTS_PREFS = "points";

    void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        if(PlayerPrefs.HasKey(POINTS_PREFS))
        {
            points = PlayerPrefs.GetInt(POINTS_PREFS);
        }
    }
    #endregion

    public int points = 0;

    public void FinishRun(int reward)
    {
        points += reward;
        PlayerPrefs.SetInt(POINTS_PREFS, points);
    }
}
