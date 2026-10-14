using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/Saw", fileName = "Saw")]
public class Saw : Item
{
    
    public int CostReduction;

    public void ShopFunction(ShopManager shopManager)
    {
        Function();
    }

    public override void Function()
    {
        RunPlayerStats.Instance.ShopManager.ShopItems.ForEach(x => x.Cost-=CostReduction);
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
    public override string Description => "Prices in shops are reduced by "+ CostReduction +"";
    public override string Rarity => "Rare";
}
