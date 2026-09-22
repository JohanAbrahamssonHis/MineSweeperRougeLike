using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomElite : Room
{
    public override string Name => "Elite Mine Room";
    public override string Description => "A game of Minesweeper with the timmer on, gives money, time and one unlock";

    public override void SetUpRoom(FloorManager floorManager)
    {
        base.SetUpRoom(floorManager);
        SetStandardNeighbours(neighbours);
        scene = "MineRoomScene";
    }

    public override void RoomFunction()
    {
        base.RoomFunction();
        
        
        RunPlayerStats.Instance.EndState = false;

        SoundManager.Instance.Play("Switch", null, true, 2f, 1.5f);
        RunPlayerStats.Instance.ActiveTimer = true;
        
        SoundManager.Instance.Play("EliteEvilLaugh", null, true, 4f);
        SceneDeterminer.LoadAddedScene(scene);
    }

    public override void LeaveRoomFunction()
    {
        RunPlayerStats rPS = RunPlayerStats.Instance;
        
        rPS.RoomCountCleared++;
        base.LeaveRoomFunction();
        
        RunPlayerStats.Instance.EndRoomSet();
    }
}
