using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class MineRoomManager : MonoBehaviour
{
    public int mines;

    public Mine minePreset;

    public Grid grid;

    private Vector2 startPos;

    public bool AfterFirstMove;

    public List<MalwarePackage> malwarePackages;
    public List<Mine> _mines;
    public delegate void afterAction(object sender, AfterActionArgs args);
    public static event afterAction afterActionEvent;

    public void Start()
    {
        RunPlayerStats.Instance.MineRoomManager = this;

        RunPlayerStats.Instance.Points = 0;
        RunPlayerStats.Instance.Heat = 0;
        
        grid.squaresXSize = (int)RunPlayerStats.Instance.GridSize.x;
        grid.squaresYSize = (int)RunPlayerStats.Instance.GridSize.y;
        grid.SetupGrid();
    }

    public void BeginLogic()
    {
        
        _mines = new List<Mine>();
        //Adds basic mines
        for (int i = 0; i < mines; i++)
        {
            GameObject mineInst = Instantiate(minePreset.gameObject);
            Mine mine = mineInst.GetComponent<Mine>();
            _mines.Add(mine);
        }

        malwarePackages = RunPlayerStats.Instance.MalwarePackages;

        //Adds mines depending on packages
        foreach (var mine in malwarePackages.SelectMany(malwarePackage => malwarePackage.mines))
        {
            GameObject mineInst = Instantiate(mine.gameObject);
            Mine tempMine = mineInst.GetComponent<Mine>();
            _mines.Add(tempMine);
        }
    }

    public void SetLogic(SquareMine square)
    {
        BeginLogic();
        startPos = square.position;
        SetMineField();
        SetNumbers();
        RevealTilesFirstMove(square);
        AfterFirstMove = true;
    }

    void SetMineField()
    {
        var minesCollection = new List<Mine>(_mines);

        foreach (var selectedMine in _mines)
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
                SquareMine selectedSquare = grid.squares[GetPostion(selectedPosition)];
                if (selectedSquare.hasMine || IsNeighbour(selectedSquare.position, startPos)) continue;
                selectedSquare.hasMine = true;
                selectedSquare.mine = selectedMine;
                selectedSquare.mine.SetPosition(selectedSquare.position);
                selectedSquare.mine.SetUpMine(this);
                selectedSquare.mine.transform.parent = selectedSquare.transform;
                //GameObject mineInst = Instantiate(selectedMine.gameObject, selectedSquare.transform);
                condition = false;
            } while (condition);
        }
    }


    void SetNumbers()
    {
        grid.squares.ForEach(x => x.hasNeighbourMine = false);
        foreach (var mine in _mines)
        {
            foreach (var neighbour in mine.neighbours)
            {
                if ((neighbour.x < 0 || neighbour.x > grid.squaresXSize - 1) ||
                    (neighbour.y < 0 || neighbour.y > grid.squaresYSize - 1)) continue;
                SquareMine square = grid.squares[GetPostion(neighbour)];
                square.hasNeighbourMine = true;
                square.number += mine.weight;
            }
        }
    }

    void ResetNumbers()
    {
        foreach (SquareMine square in grid.squares)
        {
            square.number = 0;
        }
    }

    public void RevealTile(SquareMine square)
    {
        //The chosen square is revealed
        square.squareRevealed = true; 
        
        //If this grid has a mine
        if (square.mine != null) square.mine.Activate();
        
        //If this square is neighbouring a mine, it will not do looping function
        if (square.hasNeighbourMine) return;

        //Looping function for all its neighbours, Stops if it is already revealed or has a flag. Repeats this functions. 
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (square.position.x + i < 0 || square.position.x + i > grid.squaresXSize - 1 ||
                    square.position.y + j < 0 || square.position.y + j > grid.squaresYSize - 1) continue;
                if (!grid.squares[GetPostion(new Vector2(square.position.x + i, square.position.y + j))]
                        .squareRevealed &&
                    !grid.squares[GetPostion(new Vector2(square.position.x + i, square.position.y + j))].hasFlag)
                    RevealTile(grid.squares[GetPostion(new Vector2(square.position.x + i, square.position.y + j))]);
            }
        }
    }
    
    public void RevealTilesFirstMove(SquareMine square)
    {
        //The chosen square is revealed
        square.squareRevealed = true; 

        //Reveals all neighbouring squares, those cannot have a mine in them.
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (square.position.x + i < 0 || square.position.x + i > grid.squaresXSize - 1 ||
                    square.position.y + j < 0 || square.position.y + j > grid.squaresYSize - 1) continue;
                if (!grid.squares[GetPostion(new Vector2(square.position.x + i, square.position.y + j))]
                        .squareRevealed &&
                    !grid.squares[GetPostion(new Vector2(square.position.x + i, square.position.y + j))].hasFlag)
                    RevealTile(grid.squares[GetPostion(new Vector2(square.position.x + i, square.position.y + j))]);
            }
        }
    }

    int GetPostion(Vector2 pos)
    {
        int value = (int)(pos.y) + (int)(pos.x) * (grid.squaresYSize);
        return value;
    }

    bool IsNeighbour(Vector2 selectionPos, Vector2 comparePos)
    {
        return (selectionPos.x <= comparePos.x + 1 &&
                selectionPos.x >= comparePos.x - 1 &&
                selectionPos.y <= comparePos.y + 1 &&
                selectionPos.y >= comparePos.y - 1);
    }

    public void MoveMine(Mine mine, List<Vector2> neighbours)
    {
        if (mine == null)
        {
            Debug.LogError("Mine is missing to move");
        }
        
        SquareMine currentSquare = grid.squares[GetPostion(mine.position)];
        if(currentSquare.squareRevealed || currentSquare.hasFlag) return;
        
        for (int i = neighbours.Count - 1; i >= 0; i--)
        {
            int randomNeighbour = Random.Range(0, neighbours.Count);
            Vector2 newPos = neighbours[randomNeighbour];
            
            if (newPos.x < 0 || newPos.x > grid.squaresXSize - 1 ||
                newPos.y < 0 || newPos.y > grid.squaresYSize - 1)
            {
                neighbours.RemoveAt(randomNeighbour);
                continue;
            }

            SquareMine selectedSquare = grid.squares[GetPostion(newPos)];

            if (selectedSquare.hasMine ||
                selectedSquare.squareRevealed)
            {
                neighbours.RemoveAt(randomNeighbour);
                continue;
            }
            
            selectedSquare.mine = mine;
            selectedSquare.hasMine = true;
            
            mine.transform.parent = selectedSquare.transform;
            mine.transform.position = Vector2.zero;
            mine.position = neighbours[randomNeighbour];
            
            currentSquare.mine = null;
            currentSquare.hasMine = false;
            
            return;
        }
    }

    public void CheckTiles(List<Vector2> tiles)
    {
        foreach (SquareMine square in tiles.Select(squarePos => grid.squares[GetPostion(squarePos)]))
        {
            if (square.hasNeighbourMine) return;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (square.position.x + i < 0 || square.position.x + i > grid.squaresXSize - 1 ||
                        square.position.y + j < 0 || square.position.y + j > grid.squaresYSize - 1) continue;
                    if (!grid.squares[GetPostion(new Vector2(square.position.x + i, square.position.y + j))]
                            .squareRevealed &&
                        !grid.squares[GetPostion(new Vector2(square.position.x + i, square.position.y + j))].hasFlag)
                        RevealTile(grid.squares[GetPostion(new Vector2(square.position.x + i, square.position.y + j))]);
                }
            }
        }
    }

    public void AfterActionFunction()
    {
        afterActionEvent?.Invoke(this, new AfterActionArgs());
        ResetNumbers();
        SetNumbers();
        List<SquareMine> revealedSquares = grid.squares.Where(x => x.squareRevealed).ToList();
        foreach (var square in revealedSquares.Where(x => !x.hasNeighbourMine))
        {
            RevealTile(square);
        }
    }

    public void ResetBoard()
    {
        
        foreach (var mine in _mines)
        {
            Destroy(mine.gameObject);
        }
        _mines.Clear();

        foreach (var gridSquare in grid.squares)
        {
            gridSquare.mine = null;
            gridSquare.squareRevealed = false;
            gridSquare.hasFlag = false;
            gridSquare.hasMine = false;
        }
        
        ResetNumbers();

        AfterFirstMove = false;
    }
}

public class AfterActionArgs { }
