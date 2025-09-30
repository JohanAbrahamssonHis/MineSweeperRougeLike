using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossVisualDescription : MonoBehaviour, IInteractable, ITextable
{
    public string Name => RunPlayerStats.Instance.BossModification.Name;
    public string Description => RunPlayerStats.Instance.BossModification.Description;
}
