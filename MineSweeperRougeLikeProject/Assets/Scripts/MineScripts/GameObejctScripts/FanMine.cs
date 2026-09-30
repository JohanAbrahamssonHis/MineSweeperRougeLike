using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FanMine : Mine
{

    public SFanMine fanMine;
    public Sprite fanSprite;
    public Mine copiedMine;

    public override void SetUpMine(MineRoomManager mineRoomManager) => copiedMine.SetUpMine(mineRoomManager);

    public override void MineSubscribe()
    {
        base.MineSubscribe();
        ActionEvents.Instance.OnMineRoomWin += Revert;
        copiedMine.MineSubscribe();
    }

    public override void MineUnSubscribe()
    {
        base.MineUnSubscribe();
        ActionEvents.Instance.OnMineRoomWin -= Revert;
        copiedMine.MineUnSubscribe();
    }

    public override void GlobalMineSubscribe() => copiedMine.GlobalMineSubscribe();

    public override void GlobalMineUnSubscribe() => copiedMine.GlobalMineUnSubscribe();

    public override void Activate() => copiedMine.Activate();

    public override void MineUpdate() => copiedMine.MineUpdate();

    public override void GlobalMineUpdate() => copiedMine.GlobalMineUpdate();

    public override void SendDataConnection()
    {
        base.SendDataConnection();
        copiedMine = this;
        TriggerFanTransformation();
    }

    public void TriggerFanTransformation()
    {
        List<SMine> mines = new();
        RunPlayerStats.Instance.MalwarePackages.ForEach(x=> x.mines.ForEach(y => mines.Add(y)));

        SMine sMine = GetMostCommonMine(mines);

        BecomeFanOfMine(sMine);
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
        MineData = Instantiate(sMine);
        MineData.sprite = fanSprite;

        //TODO Make copied mine of correct type, should solve all
        GameObject mineInst = new(MineData.name);
        mineInst.AddComponent(MineData.GetMineType());
        copiedMine = mineInst.GetComponent<Mine>();
        MineData.SendDataToMine(copiedMine);
        copiedMine.SendDataConnection();
    }

    public void Revert()
    {
        MineData = fanMine;

        Destroy(copiedMine.gameObject);

        copiedMine = this;
    }
}
