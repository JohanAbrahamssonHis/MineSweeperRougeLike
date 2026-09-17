using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorRoomButton : MonoBehaviour, IInteractable
{
    public Room room;

    public void Interact()
    {
        //TODO Should clean this out
        RunPlayerStats.Instance.FloorManager.currentRoom = room;
        room.SetUpRoom(RunPlayerStats.Instance.FloorManager);
        StartCoroutine(Scenetransition());
    }

    private IEnumerator Scenetransition()
    {        

        SceneDeterminer.Instance.LoadAddedSceneGarage();

        float elapsed = 0f;
        while (elapsed < 0.001f)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        room.RoomFunction();
    }
}
