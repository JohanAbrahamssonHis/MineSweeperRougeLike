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

    public override string Name => "Key";
    public override string Description => "You need one less room clear per floor";
    public override string Rarity => "UnCommon";
}
