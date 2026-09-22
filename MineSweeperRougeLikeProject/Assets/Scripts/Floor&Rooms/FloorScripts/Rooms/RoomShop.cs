using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomShop : Room
{

    public override string Name => "Shop Room";
    public override string Description => "Use money to buy items that help your run";

    public override void SetUpRoom(FloorManager floorManager)
    {
        base.SetUpRoom(floorManager);
        SetStandardNeighbours(neighbours);
        scene = "ShopScene";
    }

    public override void RoomFunction()
    {
        base.RoomFunction();
        SceneDeterminer.LoadAddedScene(scene);
    }

    public override void LeaveRoomFunction()
    {
        base.LeaveRoomFunction();
    }
}
