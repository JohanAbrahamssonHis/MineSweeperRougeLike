using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Grid : MonoBehaviour
{
    [SerializeField] public GameObject SquareGameObject;
    
    public int squaresXSize;
    public int squaresYSize;
    public float margin;
    public float squareLength;

    public List<SquareMine> squares;
    
    public Sprite[] spriteNumbers;
    
    public void Awake()
    {
        
        for (int i = 0; i < squaresXSize; i++)
        {
            for (int j = 0; j < squaresYSize; j++)
            {
                GameObject square = Instantiate(SquareGameObject, new Vector2(setOnGrid(i,squaresXSize), setOnGrid(j,squaresYSize)), quaternion.identity, transform.GetChild(0).gameObject.transform);
                SquareMine squareInfo = square.GetComponent<SquareMine>();
                squares.Add(squareInfo);
                squareInfo.position = new Vector2(i, j);
                squareInfo.Numbers = spriteNumbers;
                //squareInfo.squareRevealed = true;
                square.name = $"Square {squareInfo.position.x},{squareInfo.position.y}";
            }
        }
    
    }
    
    public void Update()
    {
        if (squares.Any(x => x.hasMine && x.squareRevealed))
        {
            //Debug.Log("Lose");
        }

        if (!squares.Any(x => !x.hasMine && !x.squareRevealed))
        {
            //Debug.Log("Win");
            SceneDeterminer.ReturnToFloor(RunPlayerStats.Instance.FloorManager.currentRoom.scene);
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
