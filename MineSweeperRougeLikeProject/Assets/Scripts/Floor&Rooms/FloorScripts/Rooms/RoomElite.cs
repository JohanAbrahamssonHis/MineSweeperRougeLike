using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomElite : Room
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

        SoundManager.Instance.Play("Switch", null, true, 2f, 1.5f);
        RunPlayerStats.Instance.ActiveTimer = true;
        
        SoundManager.Instance.Play("EliteEvilLaugh", null, true, 4f);
        SceneDeterminer.LoadAddedScene(scene);
    }

    public override void LeaveRoomFunction()
    {
        base.LeaveRoomFunction();
        RunPlayerStats rPS = RunPlayerStats.Instance;
        
        rPS.RoomCountCleared++;
        RunPlayerStats.Instance.EndRoomSet();
    }
}
