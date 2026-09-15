using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoubleMine", menuName = "ScriptableObjects/Mine/DoubleMine", order = 0)]
public class DoubleMine : Mine
{
    // Start is called before the first frame update
    public override void SetUpMine(MineRoomManager mineRoomManager)
    {
        base.SetUpMine(mineRoomManager);
        weight = 2;
        SetStandardNeighbours(neighbours);
    }

    public override string Name => "Double Mine";
    public override string Description => "Counts for Neighbouring Squares as ‘2’ mines";
    public override string Rarity => "Common";
}
