using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemonMine : Mine
{
    public override void GlobalMineSubscribe()
    {
        ActionEvents.Instance.OnHealthGain += LemonMineFunction;
    }

    public override void GlobalMineUnSubscribe()
    {
        ActionEvents.Instance.OnHealthGain -= LemonMineFunction;
    }

    public void LemonMineFunction()
    {
        RunPlayerStats.Instance.Money -= 1;
    }
}
