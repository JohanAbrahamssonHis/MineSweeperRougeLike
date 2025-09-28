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
        /*
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _boxCollider2D.offset = new Vector2(-HiderForm.rect.x, HiderForm.rect.y);
        _boxCollider2D.size = new Vector2(HiderForm.rect.width, HiderForm.rect.height);
        */
        TextVisualSingleton.Instance.textVisualObject = this;
        HiderContainer.SetActive(false);
    }

    public void SetObject(GameObject gameObjectSet, ITextable textable)
    {
        parentObject = gameObjectSet;

        transform.parent = parentObject.transform;
        transform.localPosition = Vector3.zero;
        
        SetText(textable.Name, textable.Description, textable.Rarity);

        //_textBox.
        
        //HiderForm.transform.localPosition.y;
        
        HiderContainer.SetActive(true);
    }

    public void DisableObject()
    {
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