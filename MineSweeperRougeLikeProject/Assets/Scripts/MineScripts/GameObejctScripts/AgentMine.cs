using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMine : Mine
{
    public override void MineSubscribe()
    {
        ActionEvents.Instance.OnAfterAction += AgentMineMove;
    }

    public override void MineUnSubscribe()
    {
        ActionEvents.Instance.OnAfterAction -= AgentMineMove;
    }


    void AgentMineMove()
    {
        if (this == null)
        {
            Debug.LogError("Mine is missing to move");
        }

        List<Vector2> tempNeighbours = new(neighbours);
        MineRoomManager mineRoomManager = RunPlayerStats.Instance.MineRoomManager;

        for (int i = tempNeighbours.Count - 1; i >= 0; i--)
        {
            int randomNeighbour = Random.Range(0, tempNeighbours.Count);
            int moveResult = mineRoomManager.TryMoveMine(this, tempNeighbours[randomNeighbour]);
            if (moveResult == 0)
            {
                tempNeighbours.RemoveAt(randomNeighbour);
            }
            else if (moveResult == -1)
            {
                return;
            }
        }
    }
}
