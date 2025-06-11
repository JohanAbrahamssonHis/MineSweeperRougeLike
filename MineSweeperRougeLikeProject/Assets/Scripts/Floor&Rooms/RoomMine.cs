using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMine : Room
{
    public override void SetUpRoom(FloorManager floorManager)
    {
        base.SetUpRoom(floorManager);
        SetStandardNeighbours(neighbours);
    }
}
