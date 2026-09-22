using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/Saw", fileName = "Saw")]
public class Saw : Item
{

    public void ShopFunction(ShopManager shopManager)
    {
        Function();
    }

    public override void Function()
    {
        RunPlayerStats.Instance.ShopManager.ShopItems.ForEach(x => x.Cost/=2);
    }

    public override void Bought()
    {
        base.Bought();
        Function();
    }

    public override void Join()
    {
        ActionEvents.Instance.OnShopAfter += ShopFunction;
    }

    public override void Unsubscribe()
    {
        base.Unsubscribe();
        ActionEvents.Instance.OnShopAfter -= ShopFunction;
    }

    public override string Name => "Saw";
    public override string Description => "Prices in shops are halved";
    public override string Rarity => "Rare";
}
