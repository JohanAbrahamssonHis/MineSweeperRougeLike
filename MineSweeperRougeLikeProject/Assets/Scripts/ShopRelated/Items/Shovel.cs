using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/Shovel", fileName = "Shovel")]
public class Shovel : Item
{
    public override void Function()
    {
        RunPlayerStats.Instance.Points += (int)(2*RunPlayerStats.Instance.ComboValue);
    }

    public override void Join()
    {
        ActionEvents.Instance.OnAction += Function;
    }

    public override void Unsubscribe()
    {
        base.Unsubscribe();
        ActionEvents.Instance.OnAction -= Function;
    }
}
