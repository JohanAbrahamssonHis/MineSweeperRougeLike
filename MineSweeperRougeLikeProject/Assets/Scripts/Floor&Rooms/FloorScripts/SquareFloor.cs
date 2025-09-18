using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SquareFloor : MonoBehaviour, IInteractable
{
    public int number;
    private GameObject containter;
    public bool squareRevealed;
    public bool hasRoom;
    public Room room;
    public Sprite[] Numbers;
    public Vector2 position;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _spriteRendererContainer;
    public bool hasNeighbourRoom;

    public bool hasFlag;
    private GameObject flagContainer;
    private SpriteRenderer _spriteRendererFlagContainer;

    public bool hasNeighbourShop;
    public Sprite decalSprite;
    private GameObject decalContainer;
    private SpriteRenderer _spriteRendererDecalContainer;

    public Sprite squareSpriteUnused;
    public Sprite squareSpriteUsed;
    
    // Start is called before the first frame update
    void Start()
    {
        containter = gameObject.transform.GetChild(0).gameObject;
        flagContainer = gameObject.transform.GetChild(1).gameObject;
        decalContainer = gameObject.transform.GetChild(2).gameObject;
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRendererContainer = containter.GetComponent<SpriteRenderer>();
        _spriteRendererFlagContainer = flagContainer.GetComponent<SpriteRenderer>();
        _spriteRendererDecalContainer = decalContainer.GetComponent<SpriteRenderer>();

        _spriteRendererDecalContainer.sprite = decalSprite;
    }

    // Update is called once per frame
    void Update()
    {
        _spriteRenderer.sprite = squareRevealed ? squareSpriteUsed : squareSpriteUnused;
        
        _spriteRendererContainer.sprite = hasNeighbourRoom ? hasRoom ? room.sprite : NumberSprites.Instance.GetNumberedSprite(number) : null;

        _spriteRendererContainer.sortingOrder = squareRevealed ? 1 : -1;
        
        _spriteRendererFlagContainer.gameObject.SetActive(hasFlag);
        
        decalContainer.SetActive(hasNeighbourShop && squareRevealed);
    }

    public void Interact()
    {
        if(hasFlag || squareRevealed) return;
        
        FloorManager floorManager = RunPlayerStats.Instance.FloorManager;
        
        SoundManager.Instance.Play("Action", null, true, 1);
        
        if (!floorManager.AfterFirstMove)
            floorManager.SetLogic(this);
        else
        {
            if (squareRevealed) return;
            floorManager.RevealTile(this);
            
            floorManager.AfterActionFunction();
        }
    }

    public void SecondInteract()
    {
        if (squareRevealed) return;
        hasFlag = !hasFlag;
        
        SoundManager.Instance.Play("Flag", transform, true, 1);
    }
}