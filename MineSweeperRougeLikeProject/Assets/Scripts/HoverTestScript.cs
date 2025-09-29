using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverTestScript : MonoBehaviour, IInteractable, ITextable
{
    private SpriteRenderer _renderer;
    private TextVisualObject _textVisualObject;
    public string text;

    public void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        //_textVisualObject = GetComponentInChildren<TextVisualObject>();
        //_textVisualObject.SetText(text);
        //RunPlayerStats.Instance.TextVisualObject = _textVisualObject;
    }

    /*
    public void OnMouseEnter()
    {
        HoverStart();
    }

    public void OnMouseOver()
    {
        if(_textVisualObject.isStillHovered) return;
        Hover();
    }

    public void OnMouseExit()
    {
        if(_textVisualObject.isStillHovered) return;
        HoverEnd("Sure is now not Hovering Main");
    }
    */
    

    public void HoverStart()
    {
        _renderer.color = Color.red;
        //_textVisualObject.HiderContainer.SetActive(true);
    }
    
    public void Hover()
    {
    }
    
    public void HoverEnd()
    {
        _renderer.color = Color.white;
        //_textVisualObject.HiderContainer.SetActive(false);
    }

    public string Name => "Hover Test";
    public string Description => text;
    public string Rarity => "common";
}