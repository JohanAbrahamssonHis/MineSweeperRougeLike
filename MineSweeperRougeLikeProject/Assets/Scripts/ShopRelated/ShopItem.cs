using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour, IInteractable
{
    private bool isBought;
    private int cost;
    public Item Item;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

        _spriteRenderer.sprite = Item.sprite;

        cost = Item.cost;
        isBought = false;
    }

    public void Interact()
    {
        if (cost > RunPlayerStats.Instance.Money || isBought) return;

        RunPlayerStats.Instance.Money -= cost;
        isBought = true;
        
        _spriteRenderer.color = Color.gray;
        
        Item.Join();
    }

    public void SecondInteract()
    {
    }
}
