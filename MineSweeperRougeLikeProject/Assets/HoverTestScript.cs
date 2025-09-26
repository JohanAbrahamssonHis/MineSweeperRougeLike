using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverTestScript : MonoBehaviour, IInteractable
{
    private SpriteRenderer _renderer;
    private TextVisualObject _textVisualObject;

    public void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _textVisualObject = GetComponentInChildren<TextVisualObject>();
    }

    
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
    

    public void HoverStart()
    {
        Debug.Log("Sure is hovering");
        //transform.localScale *= 2;
    }
    
    public void Hover()
    {
        _renderer.color = Color.red;
    }
    
    public void HoverEnd(string text)
    {
        Debug.Log(text);
        //transform.localScale /= 2;
        _renderer.color = Color.white;
    }
}
