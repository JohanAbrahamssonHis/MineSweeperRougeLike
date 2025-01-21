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
        for (int x = -2; x <= 2; x++)
        {
            for (int y = -2; y <= 2; y++)
            {
                neighbours.Add(new Vector2(position.x+x,position.y+y));
            }  
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
