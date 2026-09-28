using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "FanMine", menuName = "ScriptableObjects/Mine/FanMine", order = 15)]

public class SFanMine : SMine
{
    public override string Name => "Fan Mine";
    public override string Description => "At the start of a room, becomes a copy of your most common mine";
    public override string Rarity => "Rare";

    public override Type GetMineType(){return typeof(FanMine);}

    public override void SendDataToMine(Mine mine)
    {
        (mine as FanMine).fanMine = this;
        (mine as FanMine).fanSprite = this.sprite;
    }
}
