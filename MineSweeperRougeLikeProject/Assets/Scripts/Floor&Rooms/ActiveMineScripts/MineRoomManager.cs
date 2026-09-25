using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class MineRoomManager : MonoBehaviour
{
    //public int mines;

    //public Mine minePreset;

    public Grid grid;

    private Vector2 startPos;

    public bool AfterFirstMove;

    public List<MalwarePackage> malwarePackages;
    public List<Mine> _mines;

    public void OnEnable()
    {
        grid = transform.GetChild(0).GetComponent<Grid>();
        
        RunPlayerStats.Instance.MineRoomManager = this;
        
        RunPlayerStats.Instance.Points = 0;
        RunPlayerStats.Instance.Heat = 0;
        
        grid.squaresXSize = (int)RunPlayerStats.Instance.GridSize.x;
        grid.squaresYSize = (int)RunPlayerStats.Instance.GridSize.y;
        grid.SetupGrid();

        //ActionEvents.Instance.OnAfterAction += AfterActionFunction;
    }

    public void OnDisable()
    {
        //ActionEvents.Instance.OnAfterAction -= AfterActionFunction;
    }

    public void BeginLogic()
    {
        /*
        _mines = new List<Mine>();
        //Adds basic mines
        for (int i = 0; i < mines; i++)
        {
            GameObject mineInst =  Instantiate(minePreset.gameObject);
            Mine mine = mineInst.GetComponent<Mine>();
            _mines.Add(mine);
        }
        */

        malwarePackages = RunPlayerStats.Instance.MalwarePackages;

        //Adds mines depending on packages
        foreach (var mine in malwarePackages.SelectMany(malwarePackage => malwarePackage.mines))
        {
            GameObject mineInst = new(mine.name);
            mineInst.AddComponent(mine.GetMineType());
            Mine tempMine = mineInst.GetComponent<Mine>();
            tempMine.MineData = mine;
            tempMine.AddComponent<SpriteRenderer>();
            _mines.Add(tempMine);
        }
    }

    public void SetLogic(SquareMine startSquare)
    {
        BeginLogic();
        startPos = startSquare.position;
        SetMineField();
        SetNumbers();
        foreach (var square in grid.squares)
        {
            square.SetContainerSprite();
        }
        RevealTilesFirstMove(startSquare);
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

                selectedSquare.SetContainerSprite();

                condition = false;
            } while (condition);
        }
    }


    void SetNumbers()
    {
        grid.squares.ForEach(x => x.hasNeighbourMine = false);
        grid.squares.ForEach(x => x.isLongNeighbour = false);
        foreach (var mine in _mines)
        {
            foreach (var neighbour in mine.neighbours)
            {
                if (neighbour.x < 0 || neighbour.x > grid.squaresXSize - 1 ||
                    neighbour.y < 0 || neighbour.y > grid.squaresYSize - 1) continue;
                SquareMine square = grid.squares[GetPostion(neighbour)];
                square.hasNeighbourMine = true;
                square.number += mine.weight;
            }
            //Fix this
            foreach (var neighbour in mine.longnNeighbours)
            {
                if (neighbour.x < 0 || neighbour.x > grid.squaresXSize - 1 ||
                    neighbour.y < 0 || neighbour.y > grid.squaresYSize - 1) continue;
                SquareMine square = grid.squares[GetPostion(neighbour)];
                square.isLongNeighbour = true;
                square.longNumber += mine.weight;

                //ASSUMPTION: Long neighbours are neighbours of neighbours, so they will be counted as a neighbour as well.
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
            square.longNumber = 0;
        }
    }

    public void RevealTile(SquareMine square, int orderOfReveal = 0, float randomDissolve = 0.0f)
    {
        //The chosen square is revealed
        square.SetRevealed(true);
        
        if(randomDissolve == 0.0f) randomDissolve = Random.Range(0.03f,0.07f);
        square.StartDissolve(randomDissolve+0.03f*orderOfReveal);
        
        //If this grid has a mine
        square.mine?.Activate();
        
        //If this square is neighbouring a mine, it will not do looping function
        if (square.hasNeighbourMine) return;
        
        //Looping function for all its neighbours, Stops if it is already revealed or has a flag. Repeats this functions. 
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (square.position.x + i < 0 || square.position.x + i > grid.squaresXSize - 1 ||
                    square.position.y + j < 0 || square.position.y + j > grid.squaresYSize - 1) continue;

                SquareMine squareSelect =
                    grid.squares[GetPostion(new Vector2(square.position.x + i, square.position.y + j))];
                
                if (squareSelect.squareRevealed || squareSelect.hasFlag) continue;
                RevealTile(squareSelect, orderOfReveal + 1, randomDissolve);
            }
        }
    }

    private void RevealTilesFirstMove(SquareMine square)
    {
        ActionEvents.Instance.TriggerEventFirstAction();
        
        //The chosen square is revealed
        square.SetRevealed(true);
        
        int orderOfReveal = 0;
        float randomDissolve = Random.Range(0.03f,0.07f);
        square.StartDissolve(randomDissolve+0.03f*orderOfReveal);

        //Reveals all neighbouring squares, those cannot have a mine in them.
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (square.position.x + i < 0 || square.position.x + i > grid.squaresXSize - 1 ||
                    square.position.y + j < 0 || square.position.y + j > grid.squaresYSize - 1) continue;
                
                SquareMine squareSelect =
                    grid.squares[GetPostion(new Vector2(square.position.x + i, square.position.y + j))];
                
                if (squareSelect.squareRevealed || squareSelect.hasFlag) continue;
                RevealTile(squareSelect, orderOfReveal + 1, randomDissolve);
            }
        }
    }

    int GetPostion(Vector2 pos)
    {
        int value = (int)pos.y + (int)pos.x * grid.squaresYSize;
        return value;
    }

    bool IsNeighbour(Vector2 selectionPos, Vector2 comparePos)
    {
        return  selectionPos.x <= comparePos.x + 1 &&
                selectionPos.x >= comparePos.x - 1 &&
                selectionPos.y <= comparePos.y + 1 &&
                selectionPos.y >= comparePos.y - 1;
    }

    public int TryMoveMine(Mine mine, Vector2 attemptedPlacementPos)
    {
        //0 is false move, try again to move
        //1 is true move, move is able
        //-1 is no move possible, abort the move at all

        if (mine == null)
        {
            Debug.LogError("Mine is missing to move");
            return -1;
        }

        //If the mine is disabled, the game has ended or it is the first move, mines can not move and are stopped.
        if(mine.isDisabled || RunPlayerStats.Instance.EndState || !AfterFirstMove) return -1;
        
        SquareMine currentSquare = grid.squares[GetPostion(mine.position)];
        if(currentSquare.squareRevealed || currentSquare.hasFlag) return -1;

        // If the attempted placement is outside the board, it is not a valid placement
        if (attemptedPlacementPos.x < 0 || attemptedPlacementPos.x > grid.squaresXSize - 1 ||
                attemptedPlacementPos.y < 0 || attemptedPlacementPos.y > grid.squaresYSize - 1) return 0;
        
        SquareMine selectedSquare = grid.squares[GetPostion(attemptedPlacementPos)];

        // If a mine is already at the position attempted to be placed in or that square is revealed. It is not a valid placement
        if (selectedSquare.hasMine ||
            selectedSquare.squareRevealed)  return 0;
        
        selectedSquare.mine = mine;
        selectedSquare.hasMine = true;
        
        mine.transform.parent = selectedSquare.transform;
        mine.transform.position = Vector2.zero;
        mine.position = attemptedPlacementPos;
        mine.SetMineNeighbours();

        
        currentSquare.mine = null;
        currentSquare.hasMine = false;
        
        return 1;
    }

    public void SetContainters(List<Vector2> pos, Sprite sprite, bool stopAtMines)
    {
        List<SquareMine> selectedSquare = pos.Where(posSelected => !(posSelected.x < 0 || posSelected.x > grid.squaresXSize - 1 ||
                posSelected.y < 0 || posSelected.y > grid.squaresYSize - 1)).Select(x => grid.squares[GetPostion(x)]).ToList();
        
        selectedSquare.Where(i => stopAtMines && !i.hasMine).ToList().ForEach(j => j.SetContainerSprite(sprite));
    }

    public void AfterActionFunction()
    {
        ResetNumbers();
        SetNumbers();
        List<SquareMine> revealedSquares = grid.squares.Where(x => x.squareRevealed).ToList();
        foreach (var square in revealedSquares.Where(x => !x.hasNeighbourMine))
        {
            ResetRevealTile(square);
        }
        
        grid.squares.ForEach(x => x.SetContainerSprite());

        ActionEvents.Instance.TriggerEventAfterReset();

        grid.CheckWin();

        if(!AfterFirstMove) AfterFirstMove = true;
    }

    private void ResetRevealTile(SquareMine square)
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (square.position.x + i < 0 || square.position.x + i > grid.squaresXSize - 1 ||
                    square.position.y + j < 0 || square.position.y + j > grid.squaresYSize - 1) continue;
                
                SquareMine squareSelect =
                    grid.squares[GetPostion(new Vector2(square.position.x + i, square.position.y + j))];
                
                if (squareSelect.squareRevealed || squareSelect.hasFlag) continue;
                RevealTile(squareSelect);
            }
        }
    }

    public void ResetBoard()
    {
        
        foreach (var mine in _mines)
        {
            Destroy(mine.transform.gameObject);
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
