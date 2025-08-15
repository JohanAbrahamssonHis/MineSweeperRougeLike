using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatsGauge : MonoBehaviour
{
    public Image comboSprite;

    public List<Sprite> comboSprites;
    
    public Slider heatGauge;
    public float speed;
    public float timeDelay;
    public float timeDelayBase;
    public float lastHeat;

    
    
    public void Update()
    {
        float heat = RunPlayerStats.Instance.Heat;

        if (lastHeat < heat) timeDelay = timeDelayBase;
        
        lastHeat = heat;
        timeDelay -= Time.deltaTime;

        if (timeDelay <= 0) heat -= Time.deltaTime * speed;

        comboSprite.sprite = heat > 0.4f ? heat > 0.8f ? comboSprites[2] : comboSprites[1] : comboSprites[0];

        heat = Math.Clamp(heat, 0, 1);
        
        heatGauge.value = heat;

        RunPlayerStats.Instance.Heat = heat;
    }
}
