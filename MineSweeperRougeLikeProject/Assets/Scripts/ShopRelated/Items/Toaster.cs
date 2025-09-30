using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/Toaster", fileName = "Toaster")]
public class Toaster : Item
{

    public override void Function()
    {
        RunPlayerStats.Instance.HeatGain += 0.5f;
    }

    public override void Join()
    {
        Function();
    }

    public override string Name => "Toaster";
    public override string Description => "Heat gained per action is increased";
    public override string Rarity => "Rare";
}
