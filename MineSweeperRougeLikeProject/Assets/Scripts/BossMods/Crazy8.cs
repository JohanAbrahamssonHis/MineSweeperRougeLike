using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "BossMod/Crazy 8", fileName = "Crazy8")]
public class Crazy8 : BossModification
{
    private Grid grid;
    public int valueIncrease;

    public override void Modification()
    {
        grid = RunPlayerStats.Instance.MineRoomManager.grid;
    }

    public override void JoinModification()
    {
        ActionEvents.Instance.OnAfterAction += AddNumbers;
    }

    private void AddNumbers()
    {
        grid.squares.ForEach(x => x.number+=valueIncrease);
    }

    public override void UnsubscribeModification()
    {
        ActionEvents.Instance.OnAfterAction -= AddNumbers;
    }
}
