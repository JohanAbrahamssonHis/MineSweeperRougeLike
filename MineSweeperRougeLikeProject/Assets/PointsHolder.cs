using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsHolder : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    public void Start()
    {
        //numberSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        //numberSpriteRenderer.sprite = NumberSprites.Instance.GetNumberedSprite(RunPlayerStats.Instance.Points);
        text.text = RunPlayerStats.Instance.Points.ToString();
    }
}
