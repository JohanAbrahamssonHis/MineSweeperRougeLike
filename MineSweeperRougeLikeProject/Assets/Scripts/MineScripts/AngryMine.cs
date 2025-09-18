using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryMine : Mine
{
    public override void SetUpMine(MineRoomManager mineRoomManager)
    {
        base.SetUpMine(mineRoomManager);
        weight = -2;
        SetStandardNeighbours(neighbours);
    }
}
