using UnityEngine;

public class AudioPlayer : MonoBehaviour
{

    private AudioSource audioSource;

    public void PlaySound(SoundType soundType)
    {
        //MAGIC...
    }
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
