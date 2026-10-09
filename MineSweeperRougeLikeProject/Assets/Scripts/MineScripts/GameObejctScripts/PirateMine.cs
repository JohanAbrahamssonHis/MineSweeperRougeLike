using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateMine : Mine
{
    public int DamagePerMoney;

    public override void Activate()
    {
        damage = RunPlayerStats.Instance.Money/DamagePerMoney;
        base.Activate();
    }

}
