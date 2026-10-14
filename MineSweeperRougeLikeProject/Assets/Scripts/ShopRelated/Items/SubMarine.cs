using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.Mathematics;
[CreateAssetMenu(menuName = "Item/SubMarine", fileName = "SubMarine")]
public class SubMarine : Item
{
    public override string Name => "Sub-Marine";

    public override string Description => "Gain " + XRayCount + " Permantent XRays and X-Rays also Disable one Mine that was X-Rayed.";

    public override string Rarity => "Very Rare";

    public XRay xRay;

    public int XRayCount = 1;

    public override void Function()
    {
        xRay.AddOrSetAbility(XRayCount,0);
    }

    public void SubMarineFunction(EffectAbility effectAbility, SquareMine squareMine)
    {
        if(effectAbility.GetType() != typeof(XRay)) return;

        if(!RunPlayerStats.Instance.MineRoomManager.AfterFirstMove) return;

        Grid grid = RunPlayerStats.Instance.MineRoomManager.grid;

        var MineSquares = RunPlayerStats.Instance.MineRoomManager.grid.squares.Where(square => square.hasMine &&
        !square.mine.isDisabled &&
        square.position.x - 1  <= squareMine.position.x && 
        square.position.x + 1 >= squareMine.position.x &&
        square.position.y - 1 <= squareMine.position.y &&
        square.position.y + 1 >= squareMine.position.y).ToList();

        if(MineSquares.Count() <= 0) return;

        SquareMine targetSquare = MineSquares[UnityEngine.Random.Range(0, MineSquares.Count)];
        targetSquare?.SetDisabled(true);

/*
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
                if(targetSquare.hasMine) targetSquare?.SetDisabled(true);
            }
        }
        return;
        */
    }

    public override void Join()
    {
        Function();
        ActionEvents.Instance.OnEffectAbilityActivated += SubMarineFunction;
    }

    public override void Unsubscribe()
    {
        ActionEvents.Instance.OnEffectAbilityActivated -= SubMarineFunction;
    }
}
