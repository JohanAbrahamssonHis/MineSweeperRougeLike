using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedMine : Mine
{
    public override void SetUpMine(MineRoomManager mineRoomManager)
    {
        base.SetUpMine(mineRoomManager);
        weight = 1;
        damage = 2;
        SetStandardNeighbours(neighbours);
    }

    public override string Name => "Spiked Mine";
    public override string Description => "Deals 2 damage when activated instead of 1";
    public override string Rarity => "Common";
}
