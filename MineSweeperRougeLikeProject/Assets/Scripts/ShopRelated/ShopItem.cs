using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour, IInteractable
{
    private bool isBought;

    private int cost;
    
    public int Cost
    {
        get => cost;
        set => SetCost(value);
    }
    public Item Item;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _spriteRendererCost;

    public void SetUpShopItem()
    {
        _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _spriteRendererCost = transform.GetChild(2).GetComponent<SpriteRenderer>();

        _spriteRenderer.sprite = Item.sprite;

        Cost = Item.cost;
        
        isBought = false;
    }

    public void SetCost(int value)
    {
        cost = value;
        _spriteRendererCost.sprite = NumberSprites.Instance.GetNumberedSprite(cost);
    }
    

    public void Interact()
    {
        if (cost > RunPlayerStats.Instance.Money || isBought) return;

        RunPlayerStats.Instance.Money -= cost;
        isBought = true;
        
        _spriteRenderer.color = Color.gray;
        
        Item.Join();
        
        RunPlayerStats.Instance.AddItemToInventory(Item);
    }

    public void SecondInteract()
    {
    }
}
