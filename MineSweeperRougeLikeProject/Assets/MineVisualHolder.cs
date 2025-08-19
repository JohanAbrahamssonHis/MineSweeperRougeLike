using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineVisualHolder : MonoBehaviour, IInteractable
{
    public Mine mine;
    public void Interact()
    {
        RunPlayerStats.Instance.FlagMineSelected = mine;
    }
}
