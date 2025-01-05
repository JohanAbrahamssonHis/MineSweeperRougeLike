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
    public GameObject containter;
    public SquareColour currentSquareColour;
    public bool squareRevealed;
    public bool hasMine;
    public Sprite Mine;
    public Sprite[] Numbers;
    public Vector2 position;
    
    // Start is called before the first frame update
    void Start()
    {
        containter = gameObject.transform.GetChild(0).gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        containter.GetComponent<SpriteRenderer>().sprite = number == 0 ? null : hasMine ? Mine : Numbers[number-1];

        containter.GetComponent<SpriteRenderer>().sortingOrder = squareRevealed ? 1 : -1;
    }
}
