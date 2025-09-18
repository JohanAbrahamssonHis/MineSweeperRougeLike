using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class BossRoomSquare : MonoBehaviour, IInteractable
{
    private GameObject containter;
    public bool squareRevealed;
    public RoomBossMine room;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _spriteRendererContainer;
    private SpriteRenderer _spriteRendererBoss;
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

        _spriteRendererBoss = transform.GetChild(2).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _spriteRenderer.sprite = squareRevealed ? squareSpriteUsed : isActive ? squareSpriteBossActive : squareSpriteBossUnactive;

        _spriteRendererContainer.sortingOrder = squareRevealed ? 1 : -1;
        
        _spriteRendererBoss.sprite = RunPlayerStats.Instance.BossModification.sprite;
    }

    public void Interact()
    {
        if(!isActive) return;
        squareRevealed = true;
        RunPlayerStats.Instance.FloorManager.currentRoom = room;
        room.SetUpRoom(RunPlayerStats.Instance.FloorManager);
        room.RoomFunction();
    }

    public void SecondInteract()
    {
        
    }
}
