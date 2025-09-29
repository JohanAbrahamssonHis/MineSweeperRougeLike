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

    public override string Name => "Normal Mine";
    public override string Description => "A Standard Mine";
    public override string Rarity => "Common";
}
