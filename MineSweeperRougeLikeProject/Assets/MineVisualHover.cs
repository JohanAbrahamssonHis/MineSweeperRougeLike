using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineVisualHover : MonoBehaviour, IInteractable, ITextable
{
    public Mine _mine;
    public string Name => _mine.Name;
    public string Description => _mine.Description;
    public string Rarity => _mine.Rarity;
}
