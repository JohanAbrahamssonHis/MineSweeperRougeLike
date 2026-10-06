using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/Laser", fileName = "Laser")]
public class Laser : Item
{
    public override string Name => "Laser";

    public override string Description => "If you do not take damage at the end of a room, Gain 1 Temporary X-Ray.";

    public override string Rarity => "UnCommon";

    public XRay Xray;
    public int tempCount = 1;

    public bool tookNoDamage = true;

    public override void Function()
    {
        if(tookNoDamage)
        {
            Xray.AddOrSetAbility(0,tempCount);
        }
    }

    public void ResetTookDamage()
    {
        tookNoDamage = true;
    }

    public void SetTookDamage()
    {
        tookNoDamage = false;
    }


    public override void Join()
    {
        ActionEvents.Instance.OnMineRoomWin += Function;
        ActionEvents.Instance.OnAfterFirstAction += ResetTookDamage;
        ActionEvents.Instance.OnDamage += SetTookDamage;
    }
}
