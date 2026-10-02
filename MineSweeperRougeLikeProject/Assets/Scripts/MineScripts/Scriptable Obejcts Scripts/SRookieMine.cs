using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "RookieMine", menuName = "ScriptableObjects/Mine/RookieMine", order = 19)]
public class SRookieMine : SMine
{
    public override List<Vector2> GetLongNeighbours(Vector2 pos)
    {
        List<Vector2> longNeighbours = new List<Vector2>();
        MineRoomManager mRM = RunPlayerStats.Instance.MineRoomManager;
        for (int x = -mRM.grid.squaresXSize; x <= mRM.grid.squaresXSize; x++)
        {
            if(pos.x+x< 0 || pos.x+x >= mRM.grid.squaresXSize) continue;

            //Is a neighbour
            if(x <= 1 && x >=-1) continue;

            longNeighbours.Add(new Vector2(pos.x+x,pos.y));
        }
        for (int y = -mRM.grid.squaresYSize; y <= mRM.grid.squaresYSize; y++)
        {
            if(pos.y+y < 0 || pos.y+y >= mRM.grid.squaresYSize) continue;

            //Is a neighbour
            if(y <= 1 && y >=-1) continue;

            longNeighbours.Add(new Vector2(pos.x,pos.y+y));
        }
        return longNeighbours;
    }

    public override string Name => "Rookie Mine";
    public override string Description => "Neighbours all squares in its line and row";
    public override string Rarity => "UnCommon";

    public override Type GetMineType(){return typeof(RookieMine);}
}
