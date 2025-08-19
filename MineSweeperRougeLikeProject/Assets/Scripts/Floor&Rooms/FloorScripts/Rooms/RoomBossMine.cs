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
        CheckActivation();
    }

    public override void RoomFunction()
    {
        base.RoomFunction();
        RunPlayerStats.Instance.ActiveTimer = true;
        SceneDeterminer.LoadAddedScene(scene);
    }

    public override void LeaveRoomFunction()
    {
        base.LeaveRoomFunction();
        
        SceneDeterminer.LoadAddedScene("MalwarePicker");
        
        RunPlayerStats.Instance.RoomCount = 0;
        RunPlayerStats.Instance.FloorCount++;
        if (RunPlayerStats.Instance.FloorCount > 8)
        {
            RunPlayerStats.Instance.FloorCount = 0;
        }
        RunPlayerStats.Instance.FloorManager.ResetBoard();
    }

    public void CheckActivation()
    {
        if (RunPlayerStats.Instance.RoomCount  >= RunPlayerStats.Instance.RoomLock) bossRoomSquare.isActive = true;
    }
}
