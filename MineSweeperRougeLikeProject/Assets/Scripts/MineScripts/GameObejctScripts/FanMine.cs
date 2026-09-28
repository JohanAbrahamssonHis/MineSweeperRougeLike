using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FanMine : Mine
{

    public SFanMine fanMine;
    public Sprite fanSprite;

    public override void MineSubscribe()
    {
        base.MineSubscribe();
        ActionEvents.Instance.OnFirstAction += TriggerFanTransformation;
        ActionEvents.Instance.OnMineRoomWin += Revert;
    }

    public override void MineUnSubscribe()
    {
        base.MineUnSubscribe();
        ActionEvents.Instance.OnFirstAction -= TriggerFanTransformation;
        ActionEvents.Instance.OnMineRoomWin -= Revert;
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
        Debug.Log(sMine.name);
        MineData = sMine;
        sprite = fanSprite;
    }

    public void Revert()
    {
        MineData = fanMine;
    }
}
