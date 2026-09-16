using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LargeMine", menuName = "ScriptableObjects/Mine/LargeMine", order = 7)]
public class SLargeMine : SMine
{
    public override List<Vector2> GetLongNeighbours(Vector2 pos)
    {
        List<Vector2> longNeighbours = new List<Vector2>();
        List<Vector2> neighbours = GetNeighbours(pos);
        for (int x = -2; x <= 2; x++)
        {
            for (int y = -2; y <= 2; y++)
            {
                if (neighbours.Contains(new Vector2(pos.x + x, pos.y + y))) continue;
                longNeighbours.Add(new Vector2(pos.x+x, pos.y+y));
            }  
        }
        return longNeighbours;
    }



    public override string Name => "Large Mine";
    public override string Description => "Neighbours squares as a 5x5 square";
    public override string Rarity => "Rare";

    public override Type GetMineType(){return typeof(LargeMine);}

}
