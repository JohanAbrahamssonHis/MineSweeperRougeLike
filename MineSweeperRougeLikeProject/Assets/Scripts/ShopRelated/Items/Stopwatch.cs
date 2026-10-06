using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/Stopwatch", fileName = "Stopwatch")]
public class Stopwatch : Item, ITimeObject
{
    public override void Function()
    {
        RunPlayerStats.Instance.TimeGain += 20;
    }

    public override void Join()
    {
        Function();
    }

    public override string Name => "Stopwatch";
    public override string Description => "Completing Rooms give 20 more seconds to the timer";
    public override string Rarity => "Common";
}
