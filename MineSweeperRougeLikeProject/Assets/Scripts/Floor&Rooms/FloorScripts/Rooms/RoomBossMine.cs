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
        RunPlayerStats.Instance.endState = false;
        SceneDeterminer.LoadAddedScene(scene);
    }

    public override void LeaveRoomFunction()
    {
        base.LeaveRoomFunction();
        
        SceneDeterminer.LoadAddedScene("MalwarePicker");
        
        RunPlayerStats rPS = RunPlayerStats.Instance;
        
        rPS.Money += rPS.MoneyGain + rPS.Points/10;
        rPS.RoomCount = 0;
        rPS.FloorCount++;
        if (rPS.FloorCount > 8)
        {
            rPS.FloorCount = 0;
        }
        
        rPS.FloorManager.ResetBoard();
    }

    public void CheckActivation()
    {
        if (RunPlayerStats.Instance.RoomCount  >= RunPlayerStats.Instance.RoomLock) bossRoomSquare.isActive = true;
    }
}
