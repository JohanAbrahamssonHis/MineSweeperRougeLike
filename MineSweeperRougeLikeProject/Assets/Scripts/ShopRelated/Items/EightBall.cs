using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/EightBall", fileName = "EightBall")]
public class EightBall : Item
{
    public int odds = 8;

    public override void Function()
    {
        MineRoomManager mineRoomManager = RunPlayerStats.Instance.MineRoomManager;
        
        foreach (var item in mineRoomManager.grid.squares.Where(x => x.hasMine)) item.SetDisabled(Random.Range(0,odds)==0);
    }

    public override void Join()
    {
        ActionEvents.Instance.OnFirstAction += Function;
    }

    public override void Unsubscribe()
    {
        base.Unsubscribe();
        ActionEvents.Instance.OnFirstAction -= Function;
    }

    public override string Name => "Eight Ball";
    public override string Description => "Start of a round. 1 in "+odds+" chance per mine to be disabled";
    public override string Rarity => "UnCommon";
}
