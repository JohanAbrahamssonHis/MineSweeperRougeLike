using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Timmer : MonoBehaviour
{
    private float time;
    public Sprite sprite;
    public Sprite spriteActive;
    public TMP_Text textMinutes;
    public TMP_Text textSeconds;
    public Canvas canvas;
    private SpriteRenderer spriteRenderer;
    private AudioSource _audioSource;
    [SerializeField] private float beepLastTime;
    [SerializeField] private float beepInBetweenTimeBase;
    void Awake()
    {
        time = RunPlayerStats.Instance.Time;
        beepLastTime = time;
        
        RunPlayerStats.Instance.Timmer = this;

        _audioSource = transform.GetChild(1).GetComponent<AudioSource>();
        
        //Base object
        GameObject currentGameObject = new GameObject();
        currentGameObject.transform.parent = transform;
        currentGameObject.transform.localPosition = Vector3.zero;
        
        //Sprite background
        GameObject currentVisualGameObject = new GameObject();
        currentVisualGameObject.transform.parent = currentGameObject.transform;
        currentVisualGameObject.transform.localPosition = Vector3.zero;
        currentVisualGameObject.transform.localScale = new Vector3(3,3,3);
        spriteRenderer = currentVisualGameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        spriteRenderer.sortingOrder = 4;

        //Canvas
        canvas.transform.SetParent(currentGameObject.transform);

        //Time
        SetTimmer();
    }

    private void Update()
    {
        spriteRenderer.sprite = RunPlayerStats.Instance.ActiveTimer ? spriteActive : sprite;
        
        if(!RunPlayerStats.Instance.ActiveTimer) return;
        RunPlayerStats.Instance.Time -= Time.deltaTime*RunPlayerStats.Instance.TimeMult;

        if (!(beepLastTime >= RunPlayerStats.Instance.Time + beepInBetweenTimeBase)) return;
        
        float pitchSet = RunPlayerStats.Instance.Time > 60 ? 1 : (RunPlayerStats.Instance.Time > 10 ? 1.1f : 1.3f);
        //SoundManager.Instance.Play("BoomBeep", transform, true, 1f, pitchSet, false, 0.2f);
        _audioSource.pitch = pitchSet;
        _audioSource.Play();
        beepLastTime -= beepInBetweenTimeBase;
    }

    public void SetTimmer()
    {
        time = RunPlayerStats.Instance.Time;
        
        int minutes = (int)(time / 60f);
        int seconds = (int)(time-(minutes*60));
        textMinutes.text = $"{minutes:0#}";
        textSeconds.text = $"{seconds:0#}";
    }

    public void FixBeepTimmer()
    {
        beepLastTime = (int)time;
    }
}
