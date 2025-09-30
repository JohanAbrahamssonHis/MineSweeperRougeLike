using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/Plier", fileName = "Plier")]
public class Pliers : Item
{
    public override void Function()
    {
        RunPlayerStats.Instance.Health += 1;
    }

    public override void Join()
    {
        Function();
    }

    public override string Name => "Plier";
    public override string Description => "Gain one health";
    public override string Rarity => "Common";
}
