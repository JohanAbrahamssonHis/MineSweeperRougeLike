using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomMine : Room
{
    public override void SetUpRoom(FloorManager floorManager)
    {
        base.SetUpRoom(floorManager);
        SetStandardNeighbours(neighbours);
        scene = "MineRoomScene";
    }

    public override void RoomFunction()
    {
        base.RoomFunction();
        RunPlayerStats.Instance.endState = false;
        SceneDeterminer.LoadAddedScene(scene);
    }

    public override void LeaveRoomFunction()
    {
        base.LeaveRoomFunction();
        RunPlayerStats.Instance.RoomCount++;
        RunPlayerStats.Instance.Time += 30;
    }
}
