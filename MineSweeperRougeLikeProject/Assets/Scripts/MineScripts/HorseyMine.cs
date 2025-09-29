using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseyMine : Mine
{
    public override void SetUpMine(MineRoomManager mineRoomManager)
    {
        base.SetUpMine(mineRoomManager);
        weight = 1;
        SetStandardNeighbours(neighbours);
        for (int x = -2; x <= 2; x++)
        {
            if(x==0) continue;

            int y = 2 == Mathf.Abs(x) ? 1 : 2;

            for (int i = 1; i >= -2; i-=2)
            {
                y *= i;
                
                neighbours.Add(new Vector2(position.x+x,position.y+y));
                longnNeighbours.Add(new Vector2(position.x+x,position.y+y));
            }
            
        }
    }

    public override string Name => "Horsey Mine";
    public override string Description => "Neighbours squares as how a Knight in chess moves";
    public override string Rarity => "Common";
}
