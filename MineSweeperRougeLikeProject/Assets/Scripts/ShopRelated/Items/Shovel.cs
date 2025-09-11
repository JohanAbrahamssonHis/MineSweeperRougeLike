using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/Shovel", fileName = "Shovel")]
public class Shovel : Item
{
    public override void Function()
    {
        RunPlayerStats.Instance.PointsGain += 1;
    }

    public override void Join()
    {
        Function();
    }
}
