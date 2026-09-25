using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class DevilMine : Mine
{
    public float pauseDuration;
    private float timeBank;
    private float currentTime;

    public override void GlobalMineSubscribe()
    {
        base.GlobalMineSubscribe();
        RunPlayerStats.Instance.HeatGain += 0.1f;
    }

    public override void GlobalMineUnSubscribe()
    {
        base.GlobalMineSubscribe();
        RunPlayerStats.Instance.HeatGain -= 0.1f;
    }

    public void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime < timeBank + pauseDuration)return;

        RunPlayerStats.Instance.Time -= 6;
        timeBank += pauseDuration;
    }
}
