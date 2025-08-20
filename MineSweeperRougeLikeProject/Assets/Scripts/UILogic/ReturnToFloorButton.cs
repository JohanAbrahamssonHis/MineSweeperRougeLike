using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToFloorButton : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        SceneDeterminer.ReturnToFloor(RunPlayerStats.Instance.FloorManager.currentRoom.scene);
    }
}
