using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/Key", fileName = "Key")]
public class Key : Item
{
    public override void Function()
    {
        RunPlayerStats.Instance.RoomLock--;
    }

    public override void Join()
    {
        Function();
    }
}
