using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NegativeMine", menuName = "ScriptableObjects/Mine/NegativeMine", order = 0)]
public class NegativeMine : Mine
{
    public override void SetUpMine(MineRoomManager mineRoomManager)
    {
        base.SetUpMine(mineRoomManager);
        weight = -1;
        SetStandardNeighbours(neighbours);
    }

    public override string Name => "Sad Mine";
    public override string Description => "Counts for Neighbouring Squares as ‘-1’ mines";
    public override string Rarity => "UnCommon";
}
