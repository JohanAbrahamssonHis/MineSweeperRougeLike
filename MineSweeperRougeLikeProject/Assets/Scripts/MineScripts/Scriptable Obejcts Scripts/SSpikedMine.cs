using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpikedMine", menuName = "ScriptableObjects/Mine/SpikedMine", order = 2)]
public class SSpikedMine : SMine
{
    public override string Name => "Spiked Mine";
    public override string Description => "Deals 2 damage when activated instead of 1";
    public override string Rarity => "Common";

    public override Type GetMineType(){return typeof(SpikedMine);}
}
