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
    public int longNumber;
    private GameObject containter;
    private GameObject flagContainer;
    private GameObject decalContainter;
    
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
    private SpriteRenderer _spriteRendererDecalContainer;

    public Sprite DecalArrow;
    public bool isLongNeighbour;

    private SpriteRenderer _spriteRendererMineFlagRenderer;
    
    private SpriteRenderer _spriteRendererDecalContainerRenderer;
    
    public bool hasNeighbourMine;

    public Sprite squareSpriteUnused;
    public Sprite squareSpriteUsed;
    
    // Start is called before the first frame update
    void Start()
    {
        containter = gameObject.transform.GetChild(0).gameObject;
        flagContainer = gameObject.transform.GetChild(1).gameObject;
        decalContainter = gameObject.transform.GetChild(2).gameObject;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRendererContainer = containter.GetComponent<SpriteRenderer>();
        _spriteRendererFlagContainer = flagContainer.GetComponent<SpriteRenderer>();
        _spriteRendererDecalContainer = decalContainter.GetComponent<SpriteRenderer>();

        _spriteRendererMineFlagRenderer = flagContainer.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
        _spriteRendererDecalContainerRenderer = decalContainter.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        _spriteRenderer.sprite = squareRevealed ? squareSpriteUsed : squareSpriteUnused;
        
        _spriteRendererContainer.sprite = hasNeighbourMine ? hasMine ? mine.sprite : NumberSprites.Instance.GetNumberedSprite(number) : null;

        _spriteRendererContainer.sortingOrder = squareRevealed ? 1 : -1;

        _spriteRendererDecalContainer.sprite = isLongNeighbour && squareRevealed ? DecalArrow : null;
        //_spriteRendererDecalContainerRenderer.sprite = isLongNeighbour && squareRevealed ? NumberSprites.Instance.GetNumberedSprite(longNumber) : null;
        
        
        if (hasMine && mine.isDisabled)
        {
            _spriteRendererContainer.sprite = mine.sprite;
            _spriteRendererContainer.color = Color.red;
            _spriteRendererContainer.sortingOrder = 1;
        }

        _spriteRendererFlagContainer.gameObject.SetActive(hasFlag);
    }

    public void Interact()
    {
        if (hasFlag || (hasMine && mine.isDisabled) || RunPlayerStats.Instance.EndState) return;

        if (RunPlayerStats.Instance.DebugMode)
        {
            squareRevealed = true;
            return;
        }
        
        MineRoomManager mineRoomManager = RunPlayerStats.Instance.MineRoomManager;

        if (!mineRoomManager.AfterFirstMove)
        {
            ActionEvents.Instance.TriggerEventAction();
            SoundManager.Instance.Play("Click", transform, true, 1, 1 + RunPlayerStats.Instance.Heat / 2);
            mineRoomManager.SetLogic(this);
            ActionEvents.Instance.TriggerEventAfterAction();
        }
        else
        {
            if (squareRevealed) return;
            
            ActionEvents.Instance.TriggerEventAction();
            RunPlayerStats.Instance.Points += (int)(RunPlayerStats.Instance.ComboValue*RunPlayerStats.Instance.PointsGain);
            RunPlayerStats.Instance.Heat += 0.15f;
            
            SoundManager.Instance.Play("Click", transform, true, 1, 1 + RunPlayerStats.Instance.Heat / 2);
            
            mineRoomManager.RevealTile(this);

            mineRoomManager.AfterActionFunction();
            ActionEvents.Instance.TriggerEventAfterAction();
        }
    }

    public void SecondInteract()
    {
        if (squareRevealed || RunPlayerStats.Instance.EndState) return;
        hasFlag = !hasFlag;
        
        if (RunPlayerStats.Instance.DebugMode)
        {
            return;
        }
        
        ActionEvents.Instance.TriggerEventFlag();
        SoundManager.Instance.Play("Flag", transform, true, 1);
       
        _spriteRendererMineFlagRenderer.sprite = RunPlayerStats.Instance.FlagMineSelected == null ? null : RunPlayerStats.Instance.FlagMineSelected.sprite;
    }
}
