using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/Clock", fileName = "Clock")]
public class Clock : Item
{
    public override void Function()
    {
        RunPlayerStats.Instance.Time += 60;
    }

    public override void Join()
    {
        Function();
    }

    public override string Name => "Clock";
    public override string Description => "Gain a minute";
    public override string Rarity => "Common";
}
