using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToFloorButton : MonoBehaviour, IInteractable
{
    bool tempLock;
    public void Interact()
    {
        if (RunPlayerStats.Instance.FloorManager.currentRoom is RoomBossMine && !tempLock)
        {
            SceneDeterminer.ReturnToFloorBoss(RunPlayerStats.Instance.FloorManager.currentRoom.scene);
            tempLock = true;
        }
        else
        {
            SceneDeterminer.ReturnToFloor(RunPlayerStats.Instance.FloorManager.currentRoom.scene);
            tempLock = false;
        }
    }
}
