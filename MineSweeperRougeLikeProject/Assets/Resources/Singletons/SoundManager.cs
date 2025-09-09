using System;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private List<AudioClipItem> clipLibrary;

    public readonly List<SoundBite> SoundBites = new List<SoundBite>();
    public AudioSource Main { get; set; }

    [Serializable]
    public class AudioClipItem
    {
        public string clipName;
        public AudioClip audioClip;
    }

    private void OnEnable()
    {
        SoundBites.Clear();
    }

    // === NEW: Play by name ===
    public AudioSource Play(string clipName, Transform transform, bool playOnCommand = true, float timeToDestroy = 0f, float pitch = 1f, bool loop = false)
    {
        AudioClip clip = GetClipByName(clipName);
        if (clip == null)
        {
            Debug.LogWarning($"SoundManager: Clip not found: {clipName}");
            return null;
        }

        return AddSoundBite(clip, transform, playOnCommand, timeToDestroy, pitch, loop);
    }

    public void PlayMain(Transform transform)
    {
        if (Main != null) return;
        Main = Play("MainMusic", transform, true, 0,1,true);
    }
    
    private AudioClip GetClipByName(string name)
    {
        return clipLibrary.FirstOrDefault(x => x.clipName == name)?.audioClip;
    }

    #region AddSoundBite

    private AudioSource AddSoundBite(AudioClip audioClip, Transform transform, bool playOnCommand, float timeToDestory, float pitch, bool loop)
    {
        GameObject soundObject = new GameObject("Sound_" + transform.name);
        if(timeToDestory<=0) soundObject.transform.SetParent(transform);
        AudioSource soundObjectAudioSource = soundObject.AddComponent<AudioSource>();
        soundObjectAudioSource.outputAudioMixerGroup = Mixer;
        soundObjectAudioSource.pitch = pitch;
        soundObjectAudioSource.loop = loop;

        SoundBite soundObjectSoundBite = soundObject.AddComponent<SoundBite>();
        soundObjectSoundBite.AudioSource = soundObjectAudioSource;
        SoundBites.Add(soundObjectSoundBite);

        soundObjectAudioSource.clip = audioClip;
        if (playOnCommand) soundObjectAudioSource.Play();

        if (timeToDestory > 0f) Destroy(soundObject, timeToDestory);

        return soundObjectAudioSource;
    }

    #endregion
}
