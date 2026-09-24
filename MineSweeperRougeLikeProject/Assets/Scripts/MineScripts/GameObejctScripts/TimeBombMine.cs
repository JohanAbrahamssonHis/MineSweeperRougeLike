using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBombMine : Mine
{
    public override void Activate()
    {
        base.Activate();
        RunPlayerStats.Instance.Time /= 2;
    }
}
