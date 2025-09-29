using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleThreatMine : Mine
{
    // Start is called before the first frame update
    public override void SetUpMine(MineRoomManager mineRoomManager)
    {
        base.SetUpMine(mineRoomManager);
        weight = 3;
        SetStandardNeighbours(neighbours);
    }

    public override string Name => "Triple Mine";
    public override string Description => "Counts for Neighbouring Squares as ‘3’ mines";
    public override string Rarity => "UnCommon";
}
