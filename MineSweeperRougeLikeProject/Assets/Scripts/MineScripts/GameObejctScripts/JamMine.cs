using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamMine : Mine
{
    public Sprite questionMarkSprite;

    public override void MineSubscribe()
    {
        base.MineSubscribe();
        ActionEvents.Instance.OnAfterReset += JamIt;
    }

    public override void MineUnSubscribe()
    {
        base.MineSubscribe();
        ActionEvents.Instance.OnAfterReset -= JamIt;
    }

    public void JamIt()
    {
       RunPlayerStats.Instance.MineRoomManager.SetContainters(neighbours, questionMarkSprite, true);
    }
}
