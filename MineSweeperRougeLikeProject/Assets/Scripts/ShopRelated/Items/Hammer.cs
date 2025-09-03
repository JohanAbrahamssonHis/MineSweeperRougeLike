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
        
        List<Mine> mines = mineRoomManager._mines.Where(x => !x.isDisabled && !x.isActivated).ToList();
        
        
        for (int i = 0; i < _totalAmountOfDisable; i++)
        {
            if (mines.Count == 0) break;
            int number = Random.Range(0, mines.Count());
            mines[number].isDisabled = true;
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
}
