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
}
