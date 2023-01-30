using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject MainMenuCanvas;
    [SerializeField] GameObject InstructionsCanvas;
    [SerializeField] GameObject CreditsCanvas;

    AudioManager audioManager;
    ScoreKeeper scoreKeeper;

    private void Awake() 
    {
        audioManager = FindObjectOfType<AudioManager>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void Start() 
    {
        MainMenuCanvas.SetActive(true);
        InstructionsCanvas.SetActive(false);
        CreditsCanvas.SetActive(false);
        scoreKeeper.finalScoreCanvas.SetActive(false);
    }

    public void LoadInstructions()
    {
        InstructionsCanvas.SetActive(true);
        MainMenuCanvas.SetActive(false);
        CreditsCanvas.SetActive(false);
        PlayButtonSound();
    }

    public void LoadMainMenuCanvas()
    {
        MainMenuCanvas.SetActive(true);
        InstructionsCanvas.SetActive(false);
        CreditsCanvas.SetActive(false);
        PlayButtonSound();
    }

    public void LoadCreditsCanvas()
    {
        CreditsCanvas.SetActive(true);
        MainMenuCanvas.SetActive(false);
        InstructionsCanvas.SetActive(false);
        PlayButtonSound();
    }

    private void PlayButtonSound()
    {
        audioManager.PlayClip(audioManager.buttonSound, audioManager.buttonVolume);
    }
}
