using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/Stopwatch", fileName = "Stopwatch")]
public class Stopwatch : Item, ITimeObject
{
    public float timeGained = 20;

    public override void Function()
    {
        RunPlayerStats.Instance.TimeGain += timeGained;
    }

    public override void Join()
    {
        Function();
    }

    public override string Name => "Stopwatch";
    public override string Description => "Completing Rooms give +"+timeGained+" more seconds to the timer";
    public override string Rarity => "Common";
}
