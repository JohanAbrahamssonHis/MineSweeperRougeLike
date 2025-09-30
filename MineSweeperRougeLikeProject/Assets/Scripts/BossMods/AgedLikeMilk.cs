using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "BossMod/Aged like Milk", fileName = "Aged like Milk")]
public class AgedLikeMilk : BossModification
{
    public float  timeDamageLoss = 60;
    
    public override void Modification()
    {
        RunPlayerStats.Instance.isUnDamageable = true;
    }

    public override void JoinModification()
    {
        ActionEvents.Instance.OnDamage += MilkDamage;
    }

    private void MilkDamage()
    {
        RunPlayerStats.Instance.Time -= timeDamageLoss;
    }

    public override void UnsubscribeModification()
    {
        ActionEvents.Instance.OnDamage -= MilkDamage;
        RunPlayerStats.Instance.isUnDamageable = false;
    }

    public override string Description => "You can't take damage. Instead, you lose 1 minute";
}
