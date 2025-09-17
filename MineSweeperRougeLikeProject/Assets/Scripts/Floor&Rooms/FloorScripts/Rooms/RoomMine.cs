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
        
        
        RunPlayerStats.Instance.EndState = false;
        
        //if(Random.Range(0,rPS.RoomCount-rPS.RoomCountCleared-1)==0)
        
        SoundManager.Instance.Play("EnterRoomSound", null, true, 2f);
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
