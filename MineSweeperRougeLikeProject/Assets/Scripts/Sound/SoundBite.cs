using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBite : MonoBehaviour
{
    public AudioSource AudioSource;
    private void OnDestroy()
    {
        if(SoundManager.Instance.SoundBites.Contains(this)) SoundManager.Instance.SoundBites.Remove(this);
    }
}
