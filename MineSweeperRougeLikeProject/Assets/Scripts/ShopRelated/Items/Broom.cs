using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Item/Broom", fileName = "Broom")]
public class Broom : Item
{
    public override string Name => "Broom";

    public override string Description => "After " + actionLimit + " actions, disable a mine. (Currently " + (actionLimit - ActionCount) + " actions left)";

    public override string Rarity => "Uncommon";

    public int ActionCount { get; set; } = 0;
    public int actionLimit = 7;

    public override void Function()
    {
        if(ActionCount < actionLimit)  ActionCount++;
        if (ActionCount >= actionLimit)
        {
            List<SquareMine> squareMines = RunPlayerStats.Instance.MineRoomManager.grid.squares.Where(x => x.squareRevealed && x.hasMine && !x.mine.isDisabled).ToList();
            if(squareMines.Count == 0) return;
            SquareMine square = squareMines[Random.Range(0, squareMines.Count)];
            square.SetDisabled(true); // Disable a mine on the board
            ActionCount = 0; // Reset the action count
        }
    }

    public override void Join()
    {
        ActionEvents.Instance.OnAfterAction += Function;
    }

    public override void Unsubscribe()
    {
        ActionEvents.Instance.OnAfterAction -= Function;
    }
}
