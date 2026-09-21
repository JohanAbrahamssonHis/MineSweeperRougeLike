using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/Hammer", fileName = "Hammer")]
public class Hammer : Item
{
    [SerializeField] private int _totalAmountOfDisable;
    
    public override void Function()
    {
        MineRoomManager mineRoomManager = RunPlayerStats.Instance.MineRoomManager;
        
        List<SquareMine> mines = mineRoomManager.grid.squares.Where(x => x.hasMine).Where(y => !y.mine.isDisabled && !y.mine.isActivated).ToList();
        
        for (int i = 0; i < _totalAmountOfDisable; i++)
        {
            if (mines.Count == 0) break;
            int number = Random.Range(0, mines.Count());
            mines[number].SetDisabled(true);
            mines.RemoveAt(number);
        }
    }

    public override void Join()
    {
        ActionEvents.Instance.OnDamage += Function;
    }

    public override void Unsubscribe()
    {
        base.Unsubscribe();
        ActionEvents.Instance.OnDamage -= Function;
    }

    public override string Name => "Hammer";
    public override string Description => $"After taking damage, disable {_totalAmountOfDisable} random mines";
    public override string Rarity => "UnCommon";
}
