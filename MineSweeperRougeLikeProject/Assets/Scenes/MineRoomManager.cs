using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MineRoomManager : MonoBehaviour
{
    public int mines;

    public Grid grid;

    public Vector2 startPos;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        SetMineField();
        SetNumbers();
    }

    void SetMineField()
    {
        for (int i = 0; i < mines; i++)
        {
            if (grid.squares.Count(x => !x.hasMine) <= 9)
            {
                Debug.Log("too few");
                break;
            }

            bool condition = true;
            do
            {
                Vector2 selectedPosition = new Vector2(Random.Range(0, grid.squaresXSize),
                    Random.Range(0, grid.squaresYSize));
                Square selectedSquare = grid.squares[GetPostion(selectedPosition)];
                if (selectedSquare.hasMine || IsNeighbour(selectedSquare.position, startPos)) continue;
                selectedSquare.hasMine = true;
                condition = false;
            } while (condition);
        }
    }

    void SetNumbers()
    {
        foreach (Square square in grid.squares)
        {
            int mineValue = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (square.position.x + i < 0 || square.position.x + i > grid.squaresXSize - 1 ||
                        square.position.y + j < 0 || square.position.y + j > grid.squaresYSize - 1) continue;
                    if(grid.squares[GetPostion(new Vector2(square.position.x+i, square.position.y+j))].hasMine) mineValue++;
                }  
            }
            square.number = mineValue;
        }
    }

    int GetPostion(Vector2 pos)
    {
        int value = (int)(pos.y) + (int)(pos.x) * (grid.squaresXSize);
        return value;
    }
    
    bool IsNeighbour(Vector2 selectionPos, Vector2 comparePos)
    {
        return (selectionPos.x<=comparePos.x+1&&
                selectionPos.x>=comparePos.x-1&&
                selectionPos.y<=comparePos.y+1&&
                selectionPos.y>=comparePos.y-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
