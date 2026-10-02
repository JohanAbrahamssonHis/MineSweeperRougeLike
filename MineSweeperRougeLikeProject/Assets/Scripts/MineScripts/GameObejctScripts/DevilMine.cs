using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class DevilMine : Mine, ITimeObject
{
    public float pauseDuration;
    private float timeBank;
    private float currentTime;

    public override void GlobalMineSubscribe()
    {
        base.GlobalMineSubscribe();
        RunPlayerStats.Instance.Money +=6;
        RunPlayerStats.Instance.HeatGain += 0.1f;
    }

    public override void GlobalMineUnSubscribe()
    {
        base.GlobalMineUnSubscribe();
        RunPlayerStats.Instance.HeatGain -= 0.1f;
    }

    public override void GlobalMineUpdate()
    {
        base.GlobalMineUpdate();
        DevilReap();
    }

    public void DevilReap()
    {
        if(!RunPlayerStats.Instance.ActiveTimer) return;
        currentTime += Time.deltaTime;
        if (currentTime < timeBank + pauseDuration)return;

        RunPlayerStats.Instance.Time -= 6;
        timeBank = currentTime;
    }
}
