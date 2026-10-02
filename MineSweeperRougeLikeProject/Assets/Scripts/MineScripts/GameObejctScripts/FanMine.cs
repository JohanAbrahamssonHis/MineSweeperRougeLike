using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

public class FanMine : Mine
{

    public SFanMine fanMine;
    public Sprite fanSprite;
    public Mine copiedMine;

    public IMineBehavoir copiedBehaviour;

    

    public override void SetUpMine(MineRoomManager mineRoomManager) 
    {
        base.SetUpMine(mineRoomManager);
        if(copiedMine is not FanMine) copiedMine.SetUpMine(mineRoomManager);
    }

    public override void MineSubscribe()
    {
        base.MineSubscribe();
        ActionEvents.Instance.OnMineRoomWin += Revert;
        if(copiedMine is FanMine) return;
        copiedMine.MineSubscribe();
    }

    public override void MineUnSubscribe()
    {
        base.MineUnSubscribe();
        ActionEvents.Instance.OnMineRoomWin -= Revert;
        if(copiedMine is FanMine) return;
        copiedMine.MineUnSubscribe();
    }

    public override void GlobalMineSubscribe() {if(copiedMine is not FanMine) copiedMine.GlobalMineSubscribe();}

    public override void GlobalMineUnSubscribe() {if(copiedMine is not FanMine) copiedMine.GlobalMineUnSubscribe();}

    public override void Activate() {if(copiedMine is not FanMine) copiedMine.Activate();}

    public override void MineUpdate() {if(copiedMine is not FanMine) copiedMine.MineUpdate();}

    public override void GlobalMineUpdate() {if(copiedMine is not FanMine) copiedMine.GlobalMineUpdate();}

    public override void SendDataConnection()
    {
        base.SendDataConnection();
        copiedMine = this;
        TriggerFanTransformation();
    }

    public void TriggerFanTransformation()
    {
        // Unsubscribe previous behaviour.
        copiedBehaviour?.MineUnSubscribe(this);

        // Find corresponding behaviour.
      


        List<SMine> mines = new();
        RunPlayerStats.Instance.MalwarePackages.ForEach(x=> x.mines.ForEach(y => mines.Add(y)));

        SMine sMine = GetMostCommonMine(mines);

        //if fan mines are the most, we want to stop it. Otherwise we will have stackoverflow problems.
        if(sMine is SFanMine) return;

        BecomeFanOfMine(sMine);
        // Subscribe new behaviour using THIS FanMine.
        copiedBehaviour?.MineSubscribe(this);
    }

    private SMine GetMostCommonMine(IEnumerable<SMine> items)
    {
        if (items == null)
            throw new ArgumentNullException(nameof(items));

        var nonNullItems = items.Where(i => i != null);

        if (!nonNullItems.Any())
            return null;

        // Group by type, order by count, and return the first instance of the most common type
        var mostCommonGroup = nonNullItems
            .GroupBy(i => i.GetType())
            .OrderByDescending(g => g.Count())
            .First();

        return mostCommonGroup.First(); // Representative instance
    }

    public void BecomeFanOfMine(SMine sMine)
    {
        if (sMine == null || sMine is SFanMine)
        return;

        SMine smine = Instantiate(sMine);
        smine.sprite = fanSprite;
        
        // Remove previous copied component if necessary.
        if (copiedMine != null && copiedMine != this)
        {
            Destroy(copiedMine);
        }

        // Attach the new mine behaviour to THIS GameObject.
        copiedMine = (Mine)gameObject.AddComponent(smine.GetMineType());

        smine.SendDataToMine(copiedMine);
        copiedMine.SendDataConnection();
        MineData = smine;
        copiedMine.SetUpMine(RunPlayerStats.Instance.MineRoomManager);
        smine.GlobalMineAction();

        copiedMine.SetContext(this);

    }

    public void Revert()
    {
        if(MineData.isConstant) RunPlayerStats.Instance.mainComponents.DestroyGlobalMine(MineData);
        else Destroy(copiedMine);
        
        MineData = fanMine;

        copiedMine = this;
    }
}
