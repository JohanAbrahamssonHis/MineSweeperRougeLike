using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AgentMine", menuName = "ScriptableObjects/Mine/AgentMine", order = 9)]
public class SAgentMine : SMine
{

    public override Type GetMineType() { return typeof(AgentMine);}
    
    public override string Name => "Agent Mine";
    public override string Description => "After each action, moves to a Neighbouring Square";
    public override string Rarity => "Common";


/*
        if(isDisabled || RunPlayerStats.Instance.EndState || !mineRoomManager.AfterFirstMove) return;
        List<Vector2> neighboursTemp = new List<Vector2>(neighbours);
        
        SquareMine currentSquare = mineRoomManager.grid.squares[GetPostion(mine.position)];
        if(currentSquare.squareRevealed || currentSquare.hasFlag) return;
        
        for (int i = neighbours.Count - 1; i >= 0; i--)
        {
            int randomNeighbour = Random.Range(0, neighbours.Count);
            Vector2 newPos = neighbours[randomNeighbour];
            
            if (newPos.x < 0 || newPos.x > mineRoomManager.grid.squaresXSize - 1 ||
                newPos.y < 0 || newPos.y > mineRoomManager.grid.squaresYSize - 1)
            {
                neighbours.RemoveAt(randomNeighbour);
                continue;
            }

            SquareMine selectedSquare = mineRoomManager.grid.squares[GetPostion(newPos)];

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

        neighbours.Clear();
        SetStandardNeighbours(neighbours);
        */
}