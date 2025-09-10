using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public AudioClip backgroundMusic;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.playOnAwake = true;
        audioSource.loop = true;
        audioSource.volume = 0.9f;
        audioSource.reverbZoneMix = 0.9f;
        audioSource.Play();
    }
}
