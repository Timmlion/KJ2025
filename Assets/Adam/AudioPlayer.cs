using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public List<AudioClip> PopAudioClips; // List of audio clips for enemy death sounds
    private AudioSource audioSource;

    // Method to play a random EnemyDeath sound
    public void PlayEnemyDeathSound()
    {
        if (PopAudioClips == null || PopAudioClips.Count == 0)
        {
            Debug.LogError("PopAudioClips list is empty or not assigned.");
            return;
        }

        // Get a random clip from the list
        AudioClip randomClip = PopAudioClips[Random.Range(0, PopAudioClips.Count)];

        // Play the random clip
        audioSource.PlayOneShot(randomClip);

        Debug.Log($"Playing clip: {randomClip.name}");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // Play a random EnemyDeath sound
            PlayEnemyDeathSound();
        }
    }
}