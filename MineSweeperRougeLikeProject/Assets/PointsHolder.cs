using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsHolder : MonoBehaviour
{
    private SpriteRenderer numberSpriteRenderer;

    public void Start()
    {
        numberSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        numberSpriteRenderer.sprite = NumberSprites.Instance.GetNumberedSprite(RunPlayerStats.Instance.Points);
    }
}
