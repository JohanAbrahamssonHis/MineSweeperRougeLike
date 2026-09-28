using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateMine : Mine
{
    public override void Activate()
    {
        damage = RunPlayerStats.Instance.Money/5;
        base.Activate();
    }

}
