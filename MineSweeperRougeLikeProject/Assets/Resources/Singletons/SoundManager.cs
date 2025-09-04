using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Singletons/SoundManager", fileName = "SoundManager")]
public class SoundManager : ScriptableObject
{
    private static SoundManager _instance;
    
    public static SoundManager Instance {
            get
            {
                if (_instance == null) _instance = Resources.Load<SoundManager>("Singletons/SoundManager");
                return _instance;
            }
         
        }
    
    public AudioSource Main { get; set; }
    public List<AudioSource> SoundBites { get; set; }

    public void OnEnable()
    {
        SoundBites = new List<AudioSource>();
    }
    
    
    public void AddSoundBite(AudioClip audioClip, Transform transform, bool playOnCommand, float timeToDestory)
    {
        GameObject soundObject = new GameObject("Sound_"+transform.name);
        AudioSource soundObjectAudioSource = soundObject.AddComponent<AudioSource>();
        SoundBites.Add(soundObjectAudioSource);
        soundObjectAudioSource.clip = audioClip;

        if(playOnCommand) soundObjectAudioSource.Play();
        
        //TODO: Add OnDestory
        
        if(timeToDestory<=0) return;
        Destroy(soundObject, timeToDestory);
    }
    
    public void AddSoundBite(AudioClip audioClip, Transform transform, bool playOnCommand)
    {
        GameObject soundObject = new GameObject("Sound_"+transform.name);
        AudioSource soundObjectAudioSource = soundObject.AddComponent<AudioSource>();
        SoundBites.Add(soundObjectAudioSource);
        soundObjectAudioSource.clip = audioClip;

        if(playOnCommand) soundObjectAudioSource.Play();
    }

    public void OnDestroy()
    {
        //throw new NotImplementedException();
    }
}
