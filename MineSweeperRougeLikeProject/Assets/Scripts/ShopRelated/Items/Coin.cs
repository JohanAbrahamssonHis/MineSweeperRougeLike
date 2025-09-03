using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/Coin", fileName = "Coin")]
public class Coin : Item
{
    public override void Function()
    {
        RunPlayerStats.Instance.FloorManager.AddShopRoom(1);
    }

    public override void Join()
    {
        Function();
    }
}
