using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "EffectAbility/Sensor", fileName = "Sensor")]
public class Sensor : EffectAbility
{
    public Sensor()
    {
        baseCount = 1;
        count = 1;
    }

    protected override void Function(SquareMine squareMine)
    {
        if (!squareMine.hasMine)
        {
            MineRoomManager mineRoomManager = RunPlayerStats.Instance.MineRoomManager;
            if (!mineRoomManager.AfterFirstMove) mineRoomManager.SetLogic(squareMine);
            else mineRoomManager.RevealTile(squareMine);
        }
        else squareMine.mine.isDisabled = true;
    }

    public override string Name => "Sensor";
    public override string Description => "Checks a tile";
}
