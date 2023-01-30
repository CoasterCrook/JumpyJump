using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    ScoreKeeper scoreKeeper;
    AudioManager audioManager;
    CountdownTimer countdownTimer;

    private void Awake() 
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        audioManager = FindObjectOfType<AudioManager>();
        countdownTimer = FindObjectOfType<CountdownTimer>(); 
    }

    public void LoadGame()
    {   
        audioManager.PlayClip(audioManager.buttonSound, audioManager.buttonVolume);
        audioManager.gameMusic.volume = 0.75f;
        scoreKeeper.userScore = 0;
        scoreKeeper.deaths = 0;
        SceneManager.LoadScene("Level 1");
    }

    public void LoadMainMenu()
    {
        audioManager.PlayClip(audioManager.buttonSound, audioManager.buttonVolume);
        scoreKeeper.finalScoreCanvas.SetActive(false);
        scoreKeeper.gameGoing = false;
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadEndScreen()
    {
        audioManager.gameMusic.pitch = 1f;
        audioManager.gameMusic.volume = 1f;
        SceneManager.LoadScene("EndScreen");
    }
}
