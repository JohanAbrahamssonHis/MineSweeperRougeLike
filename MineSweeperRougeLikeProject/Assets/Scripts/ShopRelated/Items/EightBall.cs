using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/EightBall", fileName = "EightBall")]
public class EightBall : Item
{
    public override void Function()
    {
        MineRoomManager mineRoomManager = RunPlayerStats.Instance.MineRoomManager;
        
        
        
        mineRoomManager._mines.ForEach(x => x.isDisabled = Random.Range(0,8)==0);
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
    public override string Description => "Start of a round. 1 in 8 chance per mine to be disabled";
    public override string Rarity => "UnCommon";
}
