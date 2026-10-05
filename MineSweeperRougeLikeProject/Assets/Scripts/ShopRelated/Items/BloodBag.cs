using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/BloodBag", fileName = "BloodBag")]
public class BloodBag : Item
{
    public override string Name => "Blood Bag";

    public override string Description => "If you are at 1 health left when completing a room, gain " + HealthGain + " health";

    public override string Rarity => "UnCommon";

    public int HealthGain = 1;

    public override void Function()
    {
        if(RunPlayerStats.Instance.Health == 1)
        {
            RunPlayerStats.Instance.Health += HealthGain;
        }
    }

    public override void Join()
    {
        ActionEvents.Instance.OnMineRoomWin += Function;
    }
}
