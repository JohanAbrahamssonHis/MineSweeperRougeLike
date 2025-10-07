using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartVisual : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Room _room;
    private GarageDoorShaker _garageDoorShaker;
    void Awake()
    {
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
