using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineVisualHolder : MonoBehaviour, IInteractable, ITextable
{
    public SMine mine;
    public void Interact()
    {
        RunPlayerStats.Instance.FlagMineSelected = mine;
        if(RunPlayerStats.Instance.currentEffectAbility is Flag)
        {
            RunPlayerStats.Instance.uIEffectAbilityHolder.SetEffectVisual();
        }
    }

    public string Name => mine.Name;
    public string Description => mine.Description;
    public string Rarity => mine.Rarity;
}
