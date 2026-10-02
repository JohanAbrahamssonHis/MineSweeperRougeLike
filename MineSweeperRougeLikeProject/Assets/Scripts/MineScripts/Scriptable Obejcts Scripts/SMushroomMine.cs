using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "MushroomMine", menuName = "ScriptableObjects/Mine/MushroomMine", order = 21)]
public class SMushroomMine : SMine
{
    public override string Name => "Mushroom Mine";
    public override string Description => "If this mine is activated, add another Permanent Mushroom Mine. This mine is removed after 4 Rooms";
    public override string Rarity => "Rare";

    public override Type GetMineType(){return typeof(MushroomMine);}

    public int maxTicks = 4;

    public override void SendDataToMine(Mine mine)
    {
        (mine as MushroomMine).tickMax = maxTicks;
    }
}
