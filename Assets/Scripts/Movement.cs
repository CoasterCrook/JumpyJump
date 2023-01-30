using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float jumpHeight = 1f;

    Rigidbody rb;
    AudioManager audioManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            PlayerJump();
        }
    }

    private void PlayerJump()
    {
        rb.velocity = Vector3.up * jumpHeight;
        audioManager.PlayClip(audioManager.jumpSound, audioManager.jumpVolume);
    }
}
