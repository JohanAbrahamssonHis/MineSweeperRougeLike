using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedMine : Mine
{
    public override void SetUpMine(MineRoomManager mineRoomManager)
    {
        base.SetUpMine(mineRoomManager);
        weight = 1;
        damage = 2;
        SetStandardNeighbours(neighbours);
    }
}
