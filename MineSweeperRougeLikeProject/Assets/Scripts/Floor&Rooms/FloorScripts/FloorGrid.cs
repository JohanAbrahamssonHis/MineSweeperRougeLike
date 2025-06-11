using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class FloorGrid : MonoBehaviour
{
    [SerializeField] public GameObject SquareGameObject;
    
    public int squaresXSize;
    public int squaresYSize;
    public float margin;
    public float squareLength;

    public List<SquareFloor> squares;
    
    public Sprite[] spriteNumbers;
    
    public void Awake()
    {
        
        for (int i = 0; i < squaresXSize; i++)
        {
            for (int j = 0; j < squaresYSize; j++)
            {
                GameObject square = Instantiate(SquareGameObject, new Vector2(setOnGrid(i,squaresXSize), setOnGrid(j,squaresYSize)), quaternion.identity, transform.GetChild(0).gameObject.transform);
                SquareFloor squareInfo = square.GetComponent<SquareFloor>();
                squares.Add(squareInfo);
                squareInfo.position = new Vector2(i, j);
                squareInfo.Numbers = spriteNumbers;
                //squareInfo.squareRevealed = true;
                square.name = $"Square {squareInfo.position.x},{squareInfo.position.y}";
            }
        }
    
    }

    float setOnGrid(int index, int squaresSize)
    {
        if (squaresSize%2==0)
        {
            return (index + index * margin) - (((squaresSize - 1) / 2) + margin * ((squaresSize - 1) / 2)) -
                   squareLength / 2 - margin / 2;
        }
        
        return (index + index * margin) - (((squaresSize - 1) / 2) + margin * ((squaresSize - 1) / 2));
    }
}
