using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainMusicPlayer : MonoBehaviour
{
    public AudioClip backgroundMusic;
    public AudioMixerGroup mixerGroup;

    void Start()
    {
        if (SoundManager.Instance.Main != null)
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
        
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.playOnAwake = true;
        audioSource.loop = true;
        audioSource.volume = 0.9f;
        audioSource.reverbZoneMix = 0.9f;
        
        SoundManager.Instance.PlayMain(audioSource);
        
        SoundManager.Instance.Mixer = mixerGroup;
        SoundManager.Instance.Main.outputAudioMixerGroup = SoundManager.Instance.Mixer;
    }
    
    
}
