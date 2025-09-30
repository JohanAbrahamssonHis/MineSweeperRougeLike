using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "BossMod/Big Blind", fileName = "Big Blind")]
public class BigBlind : BossModification
{
    private float time;
    public float timeLoop = 10;
    
    private int moneyLoss;
    public float moneyLossMult = 2;
    public int moneyLossBase = 1;
    public override void Modification()
    {
        moneyLoss = moneyLossBase;
        time = 0f;
    }

    public override void JoinModification()
    {
        ActionEvents.Instance.OnMineRoomWin += SetMoneyLoss;
    }

    private void SetMoneyLoss()
    {
        for (int i = 0; i < time/timeLoop; i++)
        {
            moneyLoss = (int)(moneyLoss * moneyLossMult);
        }

        RunPlayerStats.Instance.TempMoneyGain = -moneyLoss;
    }

    public override void UnsubscribeModification()
    {
        ActionEvents.Instance.OnMineRoomWin -= SetMoneyLoss;
    }

    public override string Description => "Lose 1$ at the end of round, doubles every 10 seconds";

    public override void UpdateModification()
    {
        base.UpdateModification();
        time += Time.deltaTime;
    }
}
