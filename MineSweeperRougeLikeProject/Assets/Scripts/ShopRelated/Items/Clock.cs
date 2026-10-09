using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/Clock", fileName = "Clock")]
public class Clock : Item, ITimeObject
{
    public float timeGain = 60;

    public override void Function()
    {
        RunPlayerStats.Instance.Time += timeGain;
    }

    public override void Join()
    {
        Function();
    }

    public override string Name => "Clock";
    public override string Description => "Gain "+ timeGain/60+" minute";
    public override string Rarity => "Common";
}
