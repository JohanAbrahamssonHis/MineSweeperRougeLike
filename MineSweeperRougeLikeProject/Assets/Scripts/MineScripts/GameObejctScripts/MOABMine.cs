using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOABMine : Mine
{
    public int amountOfActionsBeforeMother;
    private int _currentAmountOfActions;
    public SMine childMineData;

    public override void MineSubscribe()
    {
        ActionEvents.Instance.OnAfterAction += Mother;
    }

    public override void MineUnSubscribe()
    {
        ActionEvents.Instance.OnAfterAction -= Mother;
    }

    public void Mother()
    {
        _currentAmountOfActions++;
        if(_currentAmountOfActions<amountOfActionsBeforeMother) return;
        
        mineRoomManager.AddTemporaryMine(childMineData);
        _currentAmountOfActions = 0;
    }
}
