using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "BossMod/Tango", fileName = "Tango")]
public class Tango : BossModification
{
    public override string Description => "After an action or effect ability, swap what the right and left click do. (After a swap, left click places flags and right click checks tiles)";

    public override void Modification()
    {
    }

    public override void JoinModification()
    {
        base.JoinModification();
        
    }
}
