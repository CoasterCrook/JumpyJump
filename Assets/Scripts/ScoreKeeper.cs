using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{   

    [SerializeField] public TextMeshProUGUI finalScoreText;
    [SerializeField] public GameObject finalScoreCanvas; 

    public int deaths = 0;
    public int userScore = 0;
    int finalScore;
    public bool gameGoing = false;
    static ScoreKeeper instance;

    private void Awake() 
    {
        ManageSingleton();    
    }

    private void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return userScore;
    }

    public int GetDeaths()
    {
        return deaths;
    }

    public void ModifyScore(int amount)
    {
        userScore += amount;
        Mathf.Clamp(userScore, 0, int.MaxValue);
    }

    public void ModifyDeaths(int amount)
    {
        deaths += amount;
        Mathf.Clamp(deaths, 0, int.MaxValue);
    }

    public float GetFinalScore()
    {
        if(userScore == 0)
            finalScore = 0;
        else if (deaths == 0)
            finalScore = userScore;
        else
            finalScore = userScore / deaths;

        return finalScore;     
    }
}
