using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemVisualHover : MonoBehaviour, IInteractable, ITextable
{
    public Item _item;
    public string Name => _item.Name;
    public string Description => _item.Description;
    public string Rarity => _item.Rarity;
}
