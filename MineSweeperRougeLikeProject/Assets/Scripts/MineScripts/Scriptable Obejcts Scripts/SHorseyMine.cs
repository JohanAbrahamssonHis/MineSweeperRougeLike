using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HorseyMine", menuName = "ScriptableObjects/Mine/HorseyMine", order = 6)]
public class SHorseyMine : SMine
{
    public override List<Vector2> GetLongNeighbours(Vector2 pos)
    {
        List<Vector2> longNeighbours = new List<Vector2>();
        for (int x = -2; x <= 2; x++)
        {
            if(x==0) continue;

            int y = 2 == Mathf.Abs(x) ? 1 : 2;

            for (int i = 1; i >= -2; i-=2)
            {
                y *= i;
                longNeighbours.Add(new Vector2(pos.x+x,pos.y+y));
            }
            
        }
        return longNeighbours;
    }

    public override string Name => "Horsey Mine";
    public override string Description => "Neighbours squares as how a Knight in chess moves";
    public override string Rarity => "Common";

    public override Type GetMineType(){return typeof(HorseyMine);}
}
