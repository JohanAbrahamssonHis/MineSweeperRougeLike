using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "BossMod/ConveyorBelt", fileName = "ConveyorBelt")]
public class ConveyorBelt : BossModification, ITimeObject
{
    public override string Description => "After every " + TimeLimit + " seconds, move all columns to the left. The leftmost column becomes the rightmost.";

    public int ActionCounter = 0;
    public int ActionCounterMax = 2;

    public float currentTime = 0;
    public float TimeLimit = 10;

    //This is if it would be action based, but I think it would be better to have it time based.
    public override void Modification()
    {
        if(ActionCounter < ActionCounterMax)
        {
            ActionCounter++;
            return;
        }
        RunPlayerStats.Instance.MineRoomManager.grid.MoveLeft(1);
        ActionCounter = 0;
    }

    public override void UpdateModification()
    {
        currentTime += Time.deltaTime;
        if(currentTime < TimeLimit) return;
        
        RunPlayerStats.Instance.MineRoomManager.grid.MoveLeft(1);
        currentTime = 0;
        
    }

    public override void JoinModification()
    {
        //ActionEvents.Instance.OnAfterAction += Modification;
    }
}
