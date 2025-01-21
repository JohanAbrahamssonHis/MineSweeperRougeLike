using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMine : Mine
{
    // Start is called before the first frame update
    public override void SetUpMine(MineRoomManager mineRoomManager)
    {
        base.SetUpMine(mineRoomManager);
        weight = 1;
        SetStandardNeighbours(neighbours);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
