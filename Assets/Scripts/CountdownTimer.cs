using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
   
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] public float startingTime = 30f;
    [SerializeField] Slider slider;

    public float currentTime;
    public bool startingTimer = true;
    static CountdownTimer instance;
    LevelManager levelManager;
    ScoreKeeper scoreKeeper;
    AudioManager audioManager;

    private void Awake() 
    {
        levelManager = FindObjectOfType<LevelManager>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        audioManager = FindObjectOfType<AudioManager>();
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
    
    public void StartTimer()
    {
        if (currentTime >= 0) // while timer is above 0
        {
            slider.gameObject.SetActive(true);
            currentTime -= 1 * Time.deltaTime;
            slider.value = currentTime / 60;
            timerText.text = currentTime.ToString("0");
            
            switch (currentTime) // changes speed of music as time runs out
            {
                case < 5:
                    audioManager.gameMusic.pitch = 1.3f;
                    break;
                case < 10:
                    audioManager.gameMusic.pitch = 1.15f;
                    break;
                case < 20:
                    audioManager.gameMusic.pitch = 1.05f;
                    break;
            }
        }
        else // when timer stops
        {
            startingTimer = false;
            levelManager.LoadEndScreen();
            slider.gameObject.SetActive(false);
            timerText.text = null;
            scoreKeeper.finalScoreText.text = $"Final Score: {scoreKeeper.GetFinalScore()}";
            scoreKeeper.finalScoreCanvas.SetActive(true);
        }
    }
}
