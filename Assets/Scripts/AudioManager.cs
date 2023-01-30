using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header ("Sounds")]
    [SerializeField] public AudioClip jumpSound;
    [SerializeField] public AudioClip scoreSound;
    [SerializeField] public AudioClip crashSound;
    [SerializeField] public AudioClip buttonSound;
    [SerializeField] public AudioClip movingSound;
    [SerializeField] public AudioClip groundSound;
    [SerializeField] public AudioSource gameMusic;

    [Header ("Volumes")]
    [SerializeField] [Range(0f, 1f)] public float jumpVolume = 1f;
    [SerializeField] [Range(0f, 1f)] public float scoreVolume = 1f;
    [SerializeField] [Range(0f, 1f)] public float crashVolume = 1f;
    [SerializeField] [Range(0f, 1f)] public float buttonVolume = 1f;
    [SerializeField] [Range(0f, 1f)] public float movingVolume = 1f;
    [SerializeField] [Range(0f, 1f)] public float groundVolume = 1f;
    [SerializeField] [Range(0f, 1f)] public float musicVolume = 1f;

    AudioSource audioSource;
    static AudioManager audioManagerInstance;

    private void Awake() 
    {
        ManageSingleton();
        audioSource = GetComponent<AudioSource>();
    }

    private void ManageSingleton()
    {
        if (audioManagerInstance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            audioManagerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() 
    {
        gameMusic.Play();
    }

    public void PlayClip(AudioClip clip, float volume) 
    {
        audioSource.PlayOneShot(clip, volume);
    }
}   

