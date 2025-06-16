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
    }

    public override void RoomFunction()
    {
        base.RoomFunction();
        SceneDeterminer.LoadAddedScene("SampleScene");
    }
}
