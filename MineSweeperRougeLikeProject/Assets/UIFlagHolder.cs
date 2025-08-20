using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFlagHolder : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        _spriteRenderer = transform.GetChild(1).transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _spriteRenderer.sprite = RunPlayerStats.Instance.FlagMineSelected == null ? null : RunPlayerStats.Instance.FlagMineSelected.sprite;
    }
}
