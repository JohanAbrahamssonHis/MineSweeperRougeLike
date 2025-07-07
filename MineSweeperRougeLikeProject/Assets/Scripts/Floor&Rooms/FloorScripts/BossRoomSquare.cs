using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BossRoomSquare : MonoBehaviour, IInteractable
{
    private GameObject containter;
    public bool squareRevealed;
    public RoomBossMine room;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _spriteRendererContainer;
    public bool isActive;
    public Sprite squareSpriteBossUnactive;
    public Sprite squareSpriteBossActive;
    public Sprite squareSpriteUsed;
    
    // Start is called before the first frame update
    void Start()
    {
        containter = gameObject.transform.GetChild(0).gameObject;
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRendererContainer = containter.GetComponent<SpriteRenderer>();
        
        _spriteRendererContainer.sprite = room.sprite;

        room.bossRoomSquare = this;
    }

    // Update is called once per frame
    void Update()
    {
        _spriteRenderer.sprite = squareRevealed ? squareSpriteUsed : isActive ? squareSpriteBossActive : squareSpriteBossUnactive;

        _spriteRendererContainer.sortingOrder = squareRevealed ? 1 : -1;
    }

    public void Interact()
    {
        if(!isActive) return;
        squareRevealed = true;
        RunPlayerStats.Instance.FloorManager.currentRoom = room;
        room.RoomFunction();
    }

    public void SecondInteract()
    {
        
    }
}
