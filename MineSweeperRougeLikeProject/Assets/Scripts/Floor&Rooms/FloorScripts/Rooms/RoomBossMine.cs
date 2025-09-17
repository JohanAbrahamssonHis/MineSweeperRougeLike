using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomBossMine : Room
{
    public BossRoomSquare bossRoomSquare;

    public void OnEnable()
    {
        SetUpRoom(RunPlayerStats.Instance.FloorManager);
    }

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
        SoundManager.Instance.Play("Switch", null, true, 2f, 1.5f);
        //SoundManager.Instance.Play("BoomBeep", null, true, 0,1,true);
        RunPlayerStats.Instance.endState = false;
        //SoundManager.Instance.Play("EliteEvilLaugh", null, true, 2f);
        
        SceneDeterminer.LoadAddedScene(scene);
    }

    public override void LeaveRoomFunction()
    {
        base.LeaveRoomFunction();
        
        SceneDeterminer.LoadAddedScene("MalwarePicker");
        
        RunPlayerStats rPS = RunPlayerStats.Instance;
        
       rPS.EndRoomSet();
        
        rPS.RoomCountCleared = 0;
        rPS.FloorCount++;
        if (rPS.FloorCount > 8)
        {
            rPS.FloorCount = 0;
        }
        
        rPS.FloorManager.ResetBoard();
    }

    public void CheckActivation()
    {
        if (RunPlayerStats.Instance.RoomCountCleared  >= RunPlayerStats.Instance.RoomLock) bossRoomSquare.isActive = true;
    }
}
