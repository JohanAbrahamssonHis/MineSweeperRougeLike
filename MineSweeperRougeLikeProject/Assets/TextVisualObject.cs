using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextVisualObject : MonoBehaviour
{
    private GameObject parentObject;
    private IInteractable parentInteract;
    public GameObject HiderContainer;
    private RectTransform HiderForm;
    private TMP_Text _textNameBox;
    private TMP_Text _textBox;
    private BoxCollider2D _boxCollider2D;
    public bool isStillHovered;
    private string text;
    void Awake()
    {
        //parentInteract = gameObject.GetComponentInParent<IInteractable>();
        HiderContainer = transform.GetChild(0).gameObject;
        HiderForm = HiderContainer.GetComponent<RectTransform>();
        _textBox = HiderContainer.transform.GetChild(1).GetComponent<TMP_Text>();
        /*
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _boxCollider2D.offset = new Vector2(-HiderForm.rect.x, HiderForm.rect.y);
        _boxCollider2D.size = new Vector2(HiderForm.rect.width, HiderForm.rect.height);
        */
        SetText(text);
        TextVisualSingleton.Instance.textVisualObject = this;
        HiderContainer.SetActive(false);
    }

    public void SetObject(GameObject gameObjectSet, ITextable textable)
    {
        parentObject = gameObjectSet.transform.parent.gameObject;
        
        SetText(textable.Description);
        HiderContainer.SetActive(true);
    }

    public void DisableObject()
    {
        HiderContainer.SetActive(false);
    }

    private void SetText(string text)
    {
        this.text = text;
        _textBox.text = this.text;
    }
    
    
}