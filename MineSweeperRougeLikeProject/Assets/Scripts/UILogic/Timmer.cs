using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    void Start()
    {
        time = RunPlayerStats.Instance.Time;

        RunPlayerStats.Instance.Timmer = this;
        
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

        //Canvas
        canvas.transform.SetParent(currentGameObject.transform);

        //Time
        SetTimmer();
    }

    private void Update()
    {
        spriteRenderer.sprite = RunPlayerStats.Instance.ActiveTimer ? spriteActive : sprite;
        
        if(!RunPlayerStats.Instance.ActiveTimer) return;
        RunPlayerStats.Instance.Time -= Time.deltaTime;
    }

    public void SetTimmer()
    {
        time = RunPlayerStats.Instance.Time;
        
        int minutes = (int)(time / 60f);
        int seconds = (int)(time-(minutes*60));
        textMinutes.text = $"{minutes:0#}";
        textSeconds.text = $"{seconds:0#}";
    }
}
