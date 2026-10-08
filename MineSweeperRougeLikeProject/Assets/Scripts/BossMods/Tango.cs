using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "BossMod/Tango", fileName = "Tango")]
public class Tango : BossModification
{
    public override string Description => "After an action or effect ability, swap what the right and left click do. (After a swap, left click places flags and right click checks tiles)";

    public override void Modification()
    {
        RunPlayerStats.Instance.IsNotSwaped = !RunPlayerStats.Instance.IsNotSwaped;
    }

    public override void JoinModification()
    {
        base.JoinModification();
        ActionEvents.Instance.OnAction += Modification;
        ActionEvents.Instance.OnEffectAbility += Modification;
    }

    public override void UnsubscribeModification()
    {
        base.UnsubscribeModification();
        ActionEvents.Instance.OnAction -= Modification;
        ActionEvents.Instance.OnEffectAbility -= Modification;
        RunPlayerStats.Instance.IsNotSwaped = true;
    }
}
