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
        ActionEvents.Instance.OnAfterReset += AddNumbers;
    }

    private void AddNumbers()
    {
        foreach (var square in grid.squares)
        {
            square.number += valueIncrease;
            square.SetContainerSprite();
        }
    }

    public override void UnsubscribeModification()
    {
        ActionEvents.Instance.OnAfterReset -= AddNumbers;
    }

    public override string Description =>
        "All numbers that are shown are ‘+8’ more than usual";
}
