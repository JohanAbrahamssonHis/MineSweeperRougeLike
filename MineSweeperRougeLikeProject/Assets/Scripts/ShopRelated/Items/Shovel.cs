using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/Shovel", fileName = "Shovel")]
public class Shovel : Item
{

    public int pointsGain = 5;

    public override void Function()
    {
        RunPlayerStats.Instance.PointsGain += pointsGain;
    }

    public override void Join()
    {
        Function();
    }

    public override string Name => "Shovel";
    public override string Description => "Gain +"+pointsGain+" extra points per action";
    public override string Rarity => "Common";
}
