using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
[CreateAssetMenu(fileName = "DevilMine", menuName = "ScriptableObjects/Mine/DevilMine", order = 11)]
public class SDevilMine : SMine, ITimeObject
{
    public override string Name => "Devil Mine";
    public override string Description => "When the timmer is on, every 6s, lose 6s. Gain 6$ and Increased Heat gain";
    public override string Rarity => "Very Rare";

    public override Type GetMineType(){return typeof(DevilMine);}

    public float pauseDuration = 6;

    public override void SendDataToMine(Mine mine){(mine as DevilMine).pauseDuration = pauseDuration;}
}
