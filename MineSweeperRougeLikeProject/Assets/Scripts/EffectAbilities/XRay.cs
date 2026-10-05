using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "EffectAbility/XRay", fileName = "XRay")]
public class XRay : EffectAbility
{
    public XRay()
    {
        baseCount = 1;
        count = 1;
    }

    protected override void Function(SquareMine squareMine)
    {
        if(!RunPlayerStats.Instance.MineRoomManager.AfterFirstMove)
        {
            return;
        }
        int x = (int)squareMine.position.x;
        int y = (int)squareMine.position.y;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                SquareMine targetSquare = RunPlayerStats.Instance.MineRoomManager.grid.squares[RunPlayerStats.Instance.MineRoomManager.GetPosition(new Vector2(x + i, y + j))];
                targetSquare?.RevealNumber();
            }
        }
    }

    public override string Name => "X-Ray";
    public override string Description => "Reveals the number, but not its content, for all tiles in a 3x3 area";
}
