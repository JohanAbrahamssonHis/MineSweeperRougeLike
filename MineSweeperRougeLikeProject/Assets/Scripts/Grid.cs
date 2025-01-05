using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] public GameObject SquareGameObject;
    
    public int squaresXSize;
    public int squaresYSize;
    public float margin;

    public List<Square> squares;

    public Sprite spriteMine;
    public Sprite[] spriteNumbers;
    
    public void Awake()
    {
        
        for (int i = 0; i < squaresXSize; i++)
        {
            for (int j = 0; j < squaresYSize; j++)
            {
                GameObject square = Instantiate(SquareGameObject, new Vector2((i+i*margin)-(((squaresXSize-1)/2)+margin*((squaresXSize-1)/2)), (j+j*margin)-(((squaresYSize-1)/2)+margin*((squaresYSize-1)/2))), quaternion.identity, gameObject.transform);
                Square squareInfo = square.AddComponent<Square>();
                squares.Add(squareInfo);
                squareInfo.position = new Vector2(i, j);
                squareInfo.Mine = spriteMine;
                squareInfo.Numbers = spriteNumbers;
                //squareInfo.squareRevealed = true;
                square.name = $"Square {squareInfo.position.x},{squareInfo.position.y}";
            }
        }
    
        Debug.Log(squares.Count);
    
    }
    
    public void Update()
    {
    }
}
