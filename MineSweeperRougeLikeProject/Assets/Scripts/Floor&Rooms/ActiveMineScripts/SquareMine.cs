using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum SquareColour
{
    Grey,
    Red,
    Gold,
    Blue,
    Orange,
    Green,
    Sheep,
}

public enum SquareType
{
    Hidden,
    Revealed
}



public class SquareMine : MonoBehaviour, IInteractable
{
    public int number;
    private GameObject containter;
    private GameObject flagContainer;
    public SquareColour currentSquareColour;
    public bool squareRevealed;
    public bool hasMine;
    public Mine mine;
    public bool hasFlag;
    public Sprite[] Numbers;
    public Vector2 position;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _spriteRendererContainer;
    private SpriteRenderer _spriteRendererFlagContainer;
    public bool hasNeighbourMine;

    public Sprite squareSpriteUnused;
    public Sprite squareSpriteUsed;
    
    // Start is called before the first frame update
    void Start()
    {
        containter = gameObject.transform.GetChild(0).gameObject;
        flagContainer = gameObject.transform.GetChild(1).gameObject;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRendererContainer = containter.GetComponent<SpriteRenderer>();
        _spriteRendererFlagContainer = flagContainer.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _spriteRenderer.sprite = squareRevealed ? squareSpriteUsed : squareSpriteUnused;
        
        _spriteRendererContainer.sprite = hasNeighbourMine ? hasMine ? mine.sprite : NumberSprites.Instance.GetNumberedSprite(number) : null;

        _spriteRendererContainer.sortingOrder = squareRevealed ? 1 : -1;

        _spriteRendererFlagContainer.sortingOrder = hasFlag ? 1 : -1;

        if (hasMine)
        {
            //Debug.Log($"Yes captain; square X {position.x}, Y {position.y}");
            //mine.GetSpriteOrder(squareRevealed ? -1 : 0);
        }
    }

    public void Interact()
    {
        if (hasFlag) return;
        
        ActionEvents.Instance.TriggerEventAction();
        RunPlayerStats.Instance.Points += (int)(RunPlayerStats.Instance.ComboValue*5);
        RunPlayerStats.Instance.Heat += 0.15f;

        MineRoomManager mineRoomManager = RunPlayerStats.Instance.MineRoomManager;
        
        if (!mineRoomManager.AfterFirstMove)
            mineRoomManager.SetLogic(this);
        else
        {
            if (squareRevealed) return;
            mineRoomManager.RevealTile(this);

            mineRoomManager.AfterActionFunction();
        }
    }

    public void SecondInteract()
    {
        if (!squareRevealed)
        {
            hasFlag = !hasFlag;
        }
    }
}
