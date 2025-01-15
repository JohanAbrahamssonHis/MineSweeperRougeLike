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



public class Square : MonoBehaviour
{
    public int number;
    private GameObject containter;
    private GameObject flagContainer;
    public SquareColour currentSquareColour;
    public bool squareRevealed;
    public bool hasMine;
    public Mine mine;
    public bool hasFlag;
    public Sprite Mine;
    public Sprite[] Numbers;
    public Vector2 position;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _spriteRendererContainer;
    private SpriteRenderer _spriteRendererFlagContainer;
    
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
        _spriteRenderer.color = squareRevealed ? Color.gray : Color.white;
        
        _spriteRendererContainer.sprite = number == 0 ? null : hasMine ? Mine : Numbers[number-1];

        _spriteRendererContainer.sortingOrder = squareRevealed ? 1 : -1;

        _spriteRendererFlagContainer.sortingOrder = hasFlag ? 1 : -1;
    }
}
