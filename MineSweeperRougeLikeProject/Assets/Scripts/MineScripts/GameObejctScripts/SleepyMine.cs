using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepyMine : Mine
{
    public override void Activate()
    {
        base.Activate();
        RunPlayerStats.Instance.Time -= 15;
    }
}
