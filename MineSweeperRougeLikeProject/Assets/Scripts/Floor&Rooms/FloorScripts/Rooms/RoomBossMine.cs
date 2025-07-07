using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomBossMine : Room
{
    public BossRoomSquare bossRoomSquare;
    
    public override void SetUpRoom(FloorManager floorManager)
    {
        base.SetUpRoom(floorManager);
        SetStandardNeighbours(neighbours);
        scene = "BossRoomScene";
    }

    public override void RoomFunction()
    {
        base.RoomFunction();
        SceneDeterminer.LoadAddedScene(scene);
    }

    public override void LeaveRoomFunction()
    {
        base.LeaveRoomFunction();
        RunPlayerStats.Instance.FloorCount = 0;
        SceneDeterminer.FloorManager.ResetBoard();
    }

    public void CheckActivation()
    {
        if (RunPlayerStats.Instance.FloorCount > 1) bossRoomSquare.isActive = true;
    }
}
