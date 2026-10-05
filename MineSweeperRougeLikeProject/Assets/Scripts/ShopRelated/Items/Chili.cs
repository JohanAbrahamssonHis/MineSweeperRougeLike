using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Chili", fileName = "Chili")]
public class Chili : Item
{
    public override string Name => "Chili";

    public override string Description => "Combo value bonuses are Doubled";

    public override string Rarity => "Rare";

    public override void Function()
    {
        RunPlayerStats.Instance.ComboValueMult *= 2;
    }

    public override void Join()
    {
        Function();
    }
}
