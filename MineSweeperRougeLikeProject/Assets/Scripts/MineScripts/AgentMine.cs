using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AgentMine", menuName = "ScriptableObjects/Mine/AgentMine", order = 0)]
public class AgentMine : Mine
{
    // Start is called before the first frame update
    public override void SetUpMine(MineRoomManager mineRoomManager)
    {
        base.SetUpMine(mineRoomManager);
        weight = 1;
        SetStandardNeighbours(neighbours);
        ActionEvents.Instance.OnAfterAction += AgentMineMove;
    }

    public override string Name => "Agent Mine";
    public override string Description => "After each action, moves to a Neighbouring Square";
    public override string Rarity => "Common";

    public void OnDestroy()
    {
        ActionEvents.Instance.OnAfterAction -= AgentMineMove;
    }

    void AgentMineMove()
    {
        if(isDisabled || RunPlayerStats.Instance.EndState || !mineRoomManager.AfterFirstMove) return;
        List<Vector2> neighboursTemp = new List<Vector2>(base.neighbours);

        mineRoomManager.MoveMine(this, neighboursTemp);
        //mineRoomManager.CheckTiles(neighbours);
        neighbours.Clear();
        SetStandardNeighbours(neighbours);
    }
}