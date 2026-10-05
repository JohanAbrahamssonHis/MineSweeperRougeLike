using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/Horse", fileName = "Horse")]
public class Horse : Item, ITimeObject
{
    public override string Name => "Horse";
    public override string Description => "If a room with timer is completed under " + (TimeLimitLong / 60) + " minutes, Gain 2$. If it was completed under " + TimeLimitShort + " seconds, Gain 5$";
    public override string Rarity => "Very Rare";

    public float timeStart = 0;

    public float TimeLimitLong = 120;
    public int MoneyGainLong = 2;
    public float TimeLimitShort = 30;
    public int MoneyGainShort = 5;


    public override void Function()
    {
        if(!RunPlayerStats.Instance.ActiveTimer) return;
        
        float timeTaken = timeStart - RunPlayerStats.Instance.Time;

        Debug.Log("Time taken: " + timeTaken + " seconds. Money gained: " + (timeTaken < TimeLimitShort ? MoneyGainShort : timeTaken < TimeLimitLong ? MoneyGainLong : 0) + "$");

        RunPlayerStats.Instance.TempMoneyGain += timeTaken < TimeLimitShort ? MoneyGainShort : timeTaken < TimeLimitLong ? MoneyGainLong : 0;   
    }

    public void StartTimer()
    {
        timeStart = RunPlayerStats.Instance.Time;
    }

    public override void Join()
    {
        ActionEvents.Instance.OnTimerActivated += StartTimer;
        ActionEvents.Instance.OnMineRoomWin += Function;
    }

}
