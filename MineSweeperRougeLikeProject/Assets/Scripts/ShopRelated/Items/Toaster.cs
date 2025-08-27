using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/Toaster", fileName = "Toaster")]
public class Toaster : Item
{

    public override void Function()
    {
        RunPlayerStats.Instance.Heat += 0.1f;
    }

    public override void Join()
    {
        ActionEvents.Instance.OnAction += Function;
    }
}
