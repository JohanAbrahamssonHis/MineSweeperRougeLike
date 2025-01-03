using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] public GameObject SquareGameObject;
    
    public float squaresXSize;
    public float squaresYSize;
    public float margin;

    public List<Square> squares;
    
    public void Start()
    {
        
        for (int i = 0; i < squaresXSize; i++)
        {
            for (int j = 0; j < squaresYSize; j++)
            {
                GameObject square = Instantiate(SquareGameObject, new Vector2((i+i*margin)-(((squaresXSize-1)/2)+margin*((squaresXSize-1)/2)), (j+j*margin)-(((squaresYSize-1)/2)+margin*((squaresYSize-1)/2))), quaternion.identity, gameObject.transform);
                Square squareInfo = square.AddComponent<Square>();
                squares.Add(squareInfo);
                squareInfo.position = new Vector2(i, j);
            }
        }
    
    
    }
    
    public void Update()
    {
    }
}
