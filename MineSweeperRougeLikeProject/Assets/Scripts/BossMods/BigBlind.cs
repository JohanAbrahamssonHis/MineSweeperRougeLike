using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "BossMod/Big Blind", fileName = "Big Blind")]
public class BigBlind : BossModification
{   
    private int moneyLoss = 1;

    public override void JoinModification()
    {
        ActionEvents.Instance.OnFlag += SetMoneyLoss;
    }

    private void SetMoneyLoss()
    {
        RunPlayerStats.Instance.Money -= moneyLoss;
    }

    public override void UnsubscribeModification()
    {
        ActionEvents.Instance.OnFlag -= SetMoneyLoss;
    }

    public override string Description => "Lose 1$ whenever you place a flag";

    public override void UpdateModification()
    {
    }

    public override void Modification()
    {
    }
}
