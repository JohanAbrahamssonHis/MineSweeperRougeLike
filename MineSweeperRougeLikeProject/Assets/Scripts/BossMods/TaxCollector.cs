using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BossMod/TaxCollector", fileName = "TaxCollector")]
public class TaxCollector : BossModification
{

    public int DamageMoney = 1;
    public int MoneyLossPercent = 50;

    public override void JoinModification()
    {
        ActionEvents.Instance.OnDamage += Modification;
    }

    public override void Modification()
    {
        RunPlayerStats.Instance.Money -= DamageMoney;
        RunPlayerStats.Instance.Money = Mathf.FloorToInt(RunPlayerStats.Instance.Money * (1 - MoneyLossPercent / 100f));
    }

    public override string Description => "Whenever you take Damage, lose 1$ and then " + MoneyLossPercent + "% of your remaining Money.";
}
