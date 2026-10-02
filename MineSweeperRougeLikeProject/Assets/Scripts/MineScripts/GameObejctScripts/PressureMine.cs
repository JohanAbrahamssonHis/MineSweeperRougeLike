using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureMine : Mine
{
    public override void GlobalMineSubscribe()
    {
        base.GlobalMineSubscribe();
        ActionEvents.Instance.OnLeaveRoom += Pressure;
    }

    public override void GlobalMineUnSubscribe()
    {
        base.MineUnSubscribe();
        ActionEvents.Instance.OnLeaveRoom -= Pressure;
    }

    private void Pressure()
    {
        Context.damage++;
    }

    public override void Activate()
    {
        Context.isActivated = true;
        if(Context.GlobalMine.damage >= RunPlayerStats.Instance.Health && RunPlayerStats.Instance.Health != 1) RunPlayerStats.Instance.Health = 1;
        else RunPlayerStats.Instance.Health -= Context.GlobalMine.damage;
        Context.GlobalMine.damage = 1;
        SoundManager.Instance.Play("Explosion", transform, true, 1);
    }
    
}
