using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomStartVisual : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Room _room;
    private GarageDoorShaker _garageDoorShaker;
    void Awake()
    {
        _room = RunPlayerStats.Instance.FloorManager.currentRoom;
        SceneDeterminer.Instance.RoomStartVisual = this;
        _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _room.sprite;
        _garageDoorShaker = GetComponent<GarageDoorShaker>();
        _garageDoorShaker.OpenDoor();
        SoundManager.Instance.Play("OpenDoor", null, true, 2f);
    }

    public void CloseDoor(string sceneName = null)
    {
        _garageDoorShaker.CloseDoor(sceneName);
        SoundManager.Instance.Play("CloseDoor", null, true, 2f);
    }

    public void OpenDoor()
    {
        _garageDoorShaker.OpenDoor();
        SoundManager.Instance.Play("OpenDoor", null, true, 2f);
    }
}
