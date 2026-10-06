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

    public override bool Function(SquareMine squareMine)
    {
        if(!RunPlayerStats.Instance.MineRoomManager.AfterFirstMove || squareMine.numberRevealed) return false;
        int x = (int)squareMine.position.x;
        int y = (int)squareMine.position.y;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                Grid grid = RunPlayerStats.Instance.MineRoomManager.grid;
                if (x + i < 0 || x + i > grid.squaresXSize - 1 ||
                    y + j < 0 || y + j > grid.squaresYSize - 1) continue;

                SquareMine targetSquare = RunPlayerStats.Instance.MineRoomManager.grid.squares.Find(square => square.position == new Vector2(x + i, y + j));
                targetSquare?.RevealNumber();
            }
        }
        return true;
    }

    public override string Name => "X-Ray";
    public override string Description => "Reveals the number, but not its content, for all tiles in a 3x3 area";
}
