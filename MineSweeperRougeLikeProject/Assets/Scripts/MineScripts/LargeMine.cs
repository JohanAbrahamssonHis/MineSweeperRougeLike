using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeMine : Mine
{
    // Start is called before the first frame update
    public override void SetUpMine(MineRoomManager mineRoomManager)
    {
        base.SetUpMine(mineRoomManager);
        weight = 1;
        SetStandardNeighbours(neighbours);
        for (int x = -2; x <= 2; x++)
        {
            for (int y = -2; y <= 2; y++)
            {
                if (neighbours.Contains(new Vector2(position.x + x, position.y + y))) continue;
                neighbours.Add(new Vector2(position.x+x,position.y+y));
                longnNeighbours.Add(new Vector2(position.x+x,position.y+y));
            }  
        }
    }

    public override string Name => "Large Mine";
    public override string Description => "Neighbours squares as a 5x5 square";
    public override string Rarity => "Rare";

}
