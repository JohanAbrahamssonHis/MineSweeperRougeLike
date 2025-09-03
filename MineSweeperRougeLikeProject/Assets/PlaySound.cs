using System;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioClip soundEffect;
    private AudioSource audioSource;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = soundEffect;
        ActionEvents.Instance.OnAction += ActionSoundEffect;
    }

    private void OnApplicationQuit()
    {
        ActionEvents.Instance.OnAction -= ActionSoundEffect;
    }

    private void ActionSoundEffect()
    {
        audioSource.pitch = 1+RunPlayerStats.Instance.Heat;
        audioSource.Play();
    }
}
