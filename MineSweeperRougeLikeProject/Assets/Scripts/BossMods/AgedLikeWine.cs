using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "BossMod/Aged like Wine", fileName = "Aged like Wine")]
public class AgedLikeWine : BossModification
{
    public int value = 1;
    public override void Modification()
    {
        RunPlayerStats.Instance.ActiveTimer = false;
        RunPlayerStats.Instance.HealthDamageModifier -= value;
    }

    public override void UnsubscribeModification()
    {
        RunPlayerStats.Instance.HealthDamageModifier += value;
    }

    public override string Description => "Timer is paused, all sources deal +1 damage";
}
