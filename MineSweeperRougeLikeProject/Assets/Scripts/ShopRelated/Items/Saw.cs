using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/Saw", fileName = "Saw")]
public class Saw : Item
{

    public void ShopFunction(ShopManager shopManager)
    {
        shopManager.ShopItems.ForEach(x => x.Cost/=2);
    }

    public override void Function()
    {
        throw new NotImplementedException();
    }

    public override void Join()
    {
        ActionEvents.Instance.OnShopAfter += ShopFunction;
        //TODO: add shop cost immediately
        //ShopFunction();
    }

    public override void Unsubscribe()
    {
        base.Unsubscribe();
        ActionEvents.Instance.OnShopAfter -= ShopFunction;
    }
}
