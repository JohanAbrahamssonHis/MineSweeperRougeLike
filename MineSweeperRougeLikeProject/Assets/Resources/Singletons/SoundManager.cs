using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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

    public AudioMixerGroup Mixer { get; set; }
    public AudioSource Main { get; set; }
    public List<SoundBite> SoundBites { get; set; }

    public void OnEnable()
    {
        SoundBites = new List<SoundBite>();
    }

    #region AddSoundBite

    public void AddSoundBite(AudioClip audioClip, Transform transform, bool playOnCommand, float timeToDestory, float pitch)
    {
        GameObject soundObject = new GameObject("Sound_"+transform.name);
        AudioSource soundObjectAudioSource = soundObject.AddComponent<AudioSource>();
        soundObjectAudioSource.outputAudioMixerGroup = Mixer;

        soundObjectAudioSource.pitch = pitch;
        
        SoundBite soundObjectSoundBite = soundObject.AddComponent<SoundBite>();
        soundObjectSoundBite.AudioSource = soundObjectAudioSource;
        SoundBites.Add(soundObjectSoundBite);
        soundObjectAudioSource.clip = audioClip;

        if(playOnCommand) soundObjectAudioSource.Play();
        
        if(timeToDestory<=0) return;
        Destroy(soundObject, timeToDestory);
    }
    public void AddSoundBite(AudioSource audioSource,AudioClip audioClip, Transform transform, bool playOnCommand, float timeToDestory)
    {
        GameObject soundObject = new GameObject("Sound_"+transform.name);
        AudioSource soundObjectAudioSource = soundObject.AddComponent<AudioSource>();
        soundObjectAudioSource.outputAudioMixerGroup = Mixer;
        CloneAudioSource(soundObjectAudioSource, audioSource);
        SoundBite soundObjectSoundBite = soundObject.AddComponent<SoundBite>();
        soundObjectSoundBite.AudioSource = soundObjectAudioSource;
        SoundBites.Add(soundObjectSoundBite);
        soundObjectAudioSource.clip = audioClip;

        if(playOnCommand) soundObjectAudioSource.Play();
        
        if(timeToDestory<=0) return;
        Destroy(soundObject, timeToDestory);
    }
    public void AddSoundBite(AudioClip audioClip, Transform transform, bool playOnCommand, float timeToDestory)
    {
        GameObject soundObject = new GameObject("Sound_"+transform.name);
        AudioSource soundObjectAudioSource = soundObject.AddComponent<AudioSource>();
        soundObjectAudioSource.outputAudioMixerGroup = Mixer;
        SoundBite soundObjectSoundBite = soundObject.AddComponent<SoundBite>();
        soundObjectSoundBite.AudioSource = soundObjectAudioSource;
        SoundBites.Add(soundObjectSoundBite);
        soundObjectAudioSource.clip = audioClip;

        if(playOnCommand) soundObjectAudioSource.Play();
        
        //TODO: Add OnDestory
        //Maybe not needed?
        
        if(timeToDestory<=0) return;
        Destroy(soundObject, timeToDestory);
    }
    public void AddSoundBite(AudioClip audioClip, Transform transform, bool playOnCommand)
    {
        GameObject soundObject = new GameObject("Sound_"+transform.name);
        AudioSource soundObjectAudioSource = soundObject.AddComponent<AudioSource>();
        soundObjectAudioSource.outputAudioMixerGroup = Mixer;
        SoundBite soundObjectSoundBite = soundObject.AddComponent<SoundBite>();
        soundObjectSoundBite.AudioSource = soundObjectAudioSource;
        SoundBites.Add(soundObjectSoundBite);
        soundObjectAudioSource.clip = audioClip;

        if(playOnCommand) soundObjectAudioSource.Play();
    }
    private void CloneAudioSource(AudioSource copy, AudioSource set)
    {
        copy.loop = set.loop;
        copy.mute = set.mute;
        copy.pitch = set.pitch;
        copy.volume = set.volume;
        copy.priority = set.priority;
        copy.panStereo = set.panStereo;
        copy.spatialBlend = set.spatialBlend;
        copy.reverbZoneMix = set.reverbZoneMix;
    }

    #endregion
    
    public void PlayMain(AudioSource audioSource)
    {
        if(Main != null) return;
        Main = audioSource;
        Main.Play();
    }
}
