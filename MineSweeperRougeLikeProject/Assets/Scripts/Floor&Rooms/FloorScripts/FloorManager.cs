using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class FloorManager : MonoBehaviour
{
    public int rooms;

    public Room roomPreset;

    public FloorGrid grid;

    private Vector2 startPos;

    public bool AfterFirstMove;
    
    public List<Room> _rooms;

    public Room currentRoom;


    //To disable
    public PlayerInput inputHandler;
    public BossRoomSquare bossRoom;

    public void Start()
    {
        SceneDeterminer.FloorManager = this;  
    }

    public void BeginLogic()
    {
        _rooms = new List<Room>();
        //Adds basic rooms
        for (int i = 0; i < rooms; i++)
        {
            GameObject mineInst = Instantiate(roomPreset.gameObject);
            Room room = mineInst.GetComponent<Room>();
            _rooms.Add(room);
        }

        /*
        //Adds mines depending on packages
        foreach (var mine in malwarePackages.SelectMany(malwarePackage => malwarePackage.mines))
        {
            GameObject mineInst = Instantiate(mine.gameObject);
            Mine tempMine = mineInst.GetComponent<Mine>();
            _mines.Add(tempMine);
        }
        */
    }

    public void SetLogic(SquareFloor square)
    {
        BeginLogic();
        startPos = square.position;
        SetRoomField();
        SetNumbers();
        RevealTilesFirstMove(square);
        AfterFirstMove = true;
        
    }

    void SetRoomField()
    {
        
        var roomsCollection = new List<Room>(_rooms);

        foreach (var selectedRoom in _rooms)
        {
            if (grid.squares.Count(x => !x.hasRoom) <= 9)
            {
                Debug.LogError("too few");
                break;
            }

            bool condition = true;
            do
            {
                Vector2 selectedPosition = new Vector2(Random.Range(0, grid.squaresXSize),
                    Random.Range(0, grid.squaresYSize));
                SquareFloor selectedSquare = grid.squares[GetPostion(selectedPosition)];
                if (selectedSquare.hasRoom || IsNeighbour(selectedSquare.position, startPos)) continue;
                selectedSquare.hasRoom = true;
                selectedSquare.room = selectedRoom;
                selectedSquare.room.SetPosition(selectedSquare.position);
                selectedSquare.room.SetUpRoom(this);
                selectedSquare.room.transform.parent = selectedSquare.transform;
                condition = false;
            } while (condition);
        }
        
    }


    void SetNumbers()
    {
        grid.squares.ForEach(x => x.hasNeighbourRoom = false);
        foreach (var room in _rooms)
        {
            foreach (var neighbour in room.neighbours)
            {
                if ((neighbour.x < 0 || neighbour.x > grid.squaresXSize - 1) ||
                    (neighbour.y < 0 || neighbour.y > grid.squaresYSize - 1)) continue;
                SquareFloor square = grid.squares[GetPostion(neighbour)];
                square.hasNeighbourRoom = true;
                square.number += 1;
            }
        }
    }

    void ResetNumbers()
    {
        foreach (SquareFloor square in grid.squares)
        {
            square.number = 0;
        }
    }

    public void RevealTile(SquareFloor square)
    {
        square.squareRevealed = true;

        if (square.hasRoom)
        {
            currentRoom = square.room;
            square.room.RoomFunction();
        }
        
        if (square.hasNeighbourRoom) return;

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (square.position.x + i < 0 || square.position.x + i > grid.squaresXSize - 1 ||
                    square.position.y + j < 0 || square.position.y + j > grid.squaresYSize - 1) continue;
                if (!grid.squares[GetPostion(new Vector2(square.position.x + i, square.position.y + j))]
                        .squareRevealed)
                    RevealTile(grid.squares[GetPostion(new Vector2(square.position.x + i, square.position.y + j))]);
            }
        }
    }
    
    public void RevealTilesFirstMove(SquareFloor square)
    {
        square.squareRevealed = true; 
        
        if(square.hasRoom) square.room.RoomFunction();
        
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (square.position.x + i < 0 || square.position.x + i > grid.squaresXSize - 1 ||
                    square.position.y + j < 0 || square.position.y + j > grid.squaresYSize - 1) continue;
                if (!grid.squares[GetPostion(new Vector2(square.position.x + i, square.position.y + j))]
                        .squareRevealed)
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

    public void CheckTiles(List<Vector2> tiles)
    {
        /*
        foreach (Square square in tiles.Select(squarePos => grid.squares[GetPostion(squarePos)]))
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
        */
    }

    public void AfterActionFunction()
    {
        ResetNumbers();
        SetNumbers();
        List<SquareFloor> revealedSquares = grid.squares.Where(x => x.squareRevealed).ToList();
        foreach (var square in revealedSquares.Where(x => !x.hasNeighbourRoom))
        {
            RevealTile(square);
        }
    }

    public void ResetBoard()
    {
        
        foreach (var mine in _rooms)
        {
            Destroy(mine.gameObject);
        }
        _rooms.Clear();

        foreach (var gridSquare in grid.squares)
        {
            gridSquare.room = null;
            gridSquare.squareRevealed = false;
            gridSquare.hasRoom = false;
        }

        bossRoom.squareRevealed = false;
        bossRoom.isActive = false;

        AfterFirstMove = false;
    }

    public void DisableFloor(bool state)
    {
        this.gameObject.SetActive(state);
        grid.gameObject.SetActive(state);
        inputHandler.gameObject.SetActive(state);
        bossRoom.gameObject.SetActive(state);
    }
}
