using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpatientMine : Mine
{
    public float explodeTime = 30;
    private float currentTime;
    private bool isActiveTimer;

    public override void MineSubscribe()
    {
        base.MineSubscribe();
        ActionEvents.Instance.OnAfterFirstAction += SetExplodeTimer;
        ActionEvents.Instance.OnAction += ResetExplodeTimer;
    }

    public override void MineUnSubscribe()
    {
        base.MineUnSubscribe();
        ActionEvents.Instance.OnAfterFirstAction -= SetExplodeTimer;
        ActionEvents.Instance.OnAction -= ResetExplodeTimer;
    }

    public override void MineUpdate()
    {
        base.MineUpdate();
        CheckIfExplode();
    }

    public void CheckIfExplode()
    {
        if(!RunPlayerStats.Instance.ActiveTimer || !isActiveTimer) return;
        currentTime += Time.deltaTime;
        if (currentTime < explodeTime || Context.isDisabled || Context.isActivated)return;

        Activate();
        Context.mineRoomManager.grid.squares.Find(square => square.position == Context.position).SetDisabled(true);
    }

    public void ResetExplodeTimer()
    {
        currentTime = 0;
    }

    public void SetExplodeTimer()
    {
        isActiveTimer = !isActiveTimer;
    }
}
