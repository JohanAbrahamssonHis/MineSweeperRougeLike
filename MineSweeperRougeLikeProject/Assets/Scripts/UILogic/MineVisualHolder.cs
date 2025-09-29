using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineVisualHolder : MonoBehaviour, IInteractable, ITextable
{
    public Mine mine;
    public void Interact()
    {
        RunPlayerStats.Instance.FlagMineSelected = mine;
    }

    public string Name => mine.Name;
    public string Description => mine.Description;
    public string Rarity => mine.Rarity;
}
