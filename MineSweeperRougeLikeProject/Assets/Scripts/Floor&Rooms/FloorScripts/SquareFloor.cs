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

    public Sprite squareSpriteUnused;
    public Sprite squareSpriteUsed;
    
    // Start is called before the first frame update
    void Start()
    {
        containter = gameObject.transform.GetChild(0).gameObject;
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRendererContainer = containter.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _spriteRenderer.sprite = squareRevealed ? squareSpriteUsed : squareSpriteUnused;
        
        _spriteRendererContainer.sprite = hasNeighbourRoom ? hasRoom ? room.sprite : NumberSprites.Instance.GetNumberedSprite(number) : null;

        _spriteRendererContainer.sortingOrder = squareRevealed ? 1 : -1;
    }

    public void Interact()
    {
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
        
    }
}