using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerManagement : MonoBehaviour
{
    [Header ("Score Amounts")]
    [SerializeField] int Score1;
    [SerializeField] int Score2;
    [SerializeField] int Score3;
    [SerializeField] int Score4;
    [SerializeField] int Score5;
    
    [SerializeField] float backSpeed = 5000f;

    [Header ("Particles")]
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem failParticles;

    [Header ("Text")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI deathsText;
    
    Movement movement;
    Rigidbody rb;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;
    AudioManager audioManager;
    CountdownTimer countdownTimer;

    bool towardsWallPressed = false;
    bool hasCollided = false;
    int currentScene;

    void Awake()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        movement = GetComponent<Movement>();
        rb = GetComponent<Rigidbody>();
        levelManager = FindObjectOfType<LevelManager>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        audioManager = FindObjectOfType<AudioManager>();
        countdownTimer = FindObjectOfType<CountdownTimer>();
    }

    private void Start() 
    {
        ChangeScoreText();
        ChangeDeathText();
        if (scoreKeeper.gameGoing == false)
        {
            countdownTimer.currentTime = countdownTimer.startingTime;
            scoreKeeper.gameGoing = true;
        }
    }

    void Update()
    {
        countdownTimer.StartTimer();
        
        if (towardsWallPressed == false)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
            {
                StopMovement();
            }
        }
        if (towardsWallPressed == true && hasCollided == false)
        {
            MoveTowardsWall();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (hasCollided) { return; }

        switch (other.gameObject.tag)
        {
            case "Ground":
                audioManager.PlayClip(audioManager.groundSound, audioManager.groundVolume);
                break;
            case "Score1":
                ModifyUserScore(Score1);
                StartSuccessSequence();
                break;
            case "Score2":
                ModifyUserScore(Score2);
                StartSuccessSequence();
                break;
            case "Score3":
                ModifyUserScore(Score3);
                StartSuccessSequence();
                break;
            case "Score4":
                ModifyUserScore(Score4);
                StartSuccessSequence();
                break;
            case "Score5":
                ModifyUserScore(Score5);
                StartSuccessSequence();
                break;
            default:
                StartFailSequence();
                break;
        }
    }

    private void StopMovement()
    {
        towardsWallPressed = true;
        movement.enabled = false;
        rb.constraints = RigidbodyConstraints.FreezePosition;
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        audioManager.PlayClip(audioManager.movingSound, audioManager.movingVolume);
    }

    private void MoveTowardsWall()
    {
        rb.AddRelativeForce(Vector3.back * backSpeed * Time.deltaTime);
    }

    private void StartSuccessSequence()
    {
        hasCollided = true;
        ChangeScoreText();
        successParticles.Play();
        audioManager.PlayClip(audioManager.scoreSound, audioManager.scoreVolume);
        Invoke("LoadCurrentScene", 1);
    }

    private void StartFailSequence()
    {
        rb.constraints = RigidbodyConstraints.FreezePosition;
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        movement.enabled = false;
        
        hasCollided = true;
        scoreKeeper.ModifyDeaths(1);
        ChangeDeathText();
        failParticles.Play();
        audioManager.PlayClip(audioManager.crashSound, audioManager.crashVolume);
        Invoke("LoadCurrentScene", 1);
    }

    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(currentScene);
    }

    public void ChangeScoreText() 
    {
        scoreText.text = $"Current Score:\n {scoreKeeper.GetScore()}";
    }

    public void ChangeDeathText()
    {
        deathsText.text = $"Deaths: {scoreKeeper.GetDeaths()}";
    }
    
    private void ModifyUserScore(int score)
    {
        scoreKeeper.ModifyScore(score);
    }
}
