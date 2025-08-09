using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/Shovel", fileName = "Shovel")]
public class Shovel : Item
{
    public override void Function()
    {
        Debug.Log("hee");
    }

    public override void Join()
    {
        Debug.Log("Would join here");
    }
}
