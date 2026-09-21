using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorRoomButton : MonoBehaviour, IInteractable
{
    public Room room;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject lockObject;
    private bool isLocked;

    public void Interact()
    {
        if(isLocked) return;
        RunPlayerStats.Instance.FloorManager.currentRoom = room;
        room.SetUpRoom(RunPlayerStats.Instance.FloorManager);
        StartCoroutine(Scenetransition());
    }

    public void SetVisual(bool isLocked = false)
    {
        spriteRenderer.sprite = room.sprite;
        spriteRenderer.color = isLocked ? Color.grey : Color.white;
        this.isLocked = isLocked;
        lockObject.SetActive(isLocked);
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
