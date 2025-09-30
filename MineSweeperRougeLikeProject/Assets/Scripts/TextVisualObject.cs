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
    private TMP_Text _textBoxRarity;
    private BoxCollider2D _boxCollider2D;
    private GameObject baseParent;
    public bool isStillHovered;
    private string text;
    void Awake()
    {
        //parentInteract = gameObject.GetComponentInParent<IInteractable>();
        HiderContainer = transform.GetChild(0).gameObject;
        HiderForm = HiderContainer.GetComponent<RectTransform>();
        _textNameBox = HiderContainer.transform.GetChild(1).GetComponent<TMP_Text>();
        _textBox = HiderContainer.transform.GetChild(2).GetComponent<TMP_Text>();
        _textBoxRarity = HiderContainer.transform.GetChild(3).GetComponent<TMP_Text>();
        baseParent = transform.parent.gameObject;
        /*
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _boxCollider2D.offset = new Vector2(-HiderForm.rect.x, HiderForm.rect.y);
        _boxCollider2D.size = new Vector2(HiderForm.rect.width, HiderForm.rect.height);
        */
        TextVisualSingleton.Instance.textVisualObject = this;
        HiderContainer.SetActive(false);
        DontDestroyOnLoad(baseParent);
    }

    public void SetObject(GameObject gameObjectSet, ITextable textable)
    {
        parentObject = gameObjectSet;

        transform.parent = parentObject.transform;
        transform.localPosition = Vector3.zero;
        
        SetText(textable.Name, textable.Description, textable.Rarity);
        
        //HiderForm.rect.height = 2 + _textBox.rectTransform.rect.height;
        
        HiderForm.sizeDelta = new Vector2(
            HiderForm.sizeDelta.x, // Keep the current width
            _textNameBox.rectTransform.rect.height + _textBox.preferredHeight + (_textBoxRarity.text=="" ? 0.5f : _textBoxRarity.rectTransform.rect.height)
        );

        HiderForm.localPosition = new Vector2(
            0, 
            -(HiderForm.sizeDelta.y/2+0.5f)
        );

        _textBox.transform.localPosition = new Vector2(_textBox.transform.localPosition.x, (_textBoxRarity.text == "" ? -0.25f : 0));
        
        HiderContainer.SetActive(true);
        transform.parent = baseParent.transform;
    }

    public void DisableObject()
    {
        parentObject = null;
        HiderContainer.SetActive(false);
    }

    private void SetText(string name, string text, string rarity = "")
    {
        this.text = text;
        _textNameBox.text = name;
        _textBox.text = this.text;
        //If rarity is absent, in future should change here
        _textBoxRarity.text = rarity;
    }
    
    
}