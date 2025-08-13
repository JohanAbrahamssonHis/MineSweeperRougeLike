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
    private SpriteRenderer _spriteRendererCost;

    private void Start()
    {
        _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _spriteRendererCost = transform.GetChild(2).GetComponent<SpriteRenderer>();

        _spriteRenderer.sprite = Item.sprite;

        cost = Item.cost;
        _spriteRendererCost.sprite = NumberSprites.Instance.GetNumberedSprite(cost);
        
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
