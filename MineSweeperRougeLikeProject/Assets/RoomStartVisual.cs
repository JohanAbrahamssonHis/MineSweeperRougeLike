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
        _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _room.sprite;
        _garageDoorShaker = GetComponent<GarageDoorShaker>();
        _garageDoorShaker.OpenDoor();
    }

    public void CloseDoor()
    {
        _garageDoorShaker.CloseDoor();
    }
}
