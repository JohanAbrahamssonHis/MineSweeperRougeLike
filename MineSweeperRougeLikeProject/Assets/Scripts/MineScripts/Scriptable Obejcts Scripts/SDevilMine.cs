using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
[CreateAssetMenu(fileName = "DevilMine", menuName = "ScriptableObjects/Mine/DevilMine", order = 11)]
public class SDevilMine : SMine
{
    public override string Name => "Devil Mine";
    public override string Description => "Every 6s, lose 6s. Inscreased Heat gain";
    public override string Rarity => "Very Rare";

    public override Type GetMineType(){return typeof(DevilMine);}

    public float pauseDuration = 6;

    public override void SendDataToMine(Mine mine){(mine as DevilMine).pauseDuration = pauseDuration;}
}
