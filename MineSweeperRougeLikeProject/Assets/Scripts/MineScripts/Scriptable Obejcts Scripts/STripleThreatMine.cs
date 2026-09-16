using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TripleThreatMine", menuName = "ScriptableObjects/Mine/TripleThreatMine", order = 3)]
public class STripleThreatMine : SMine
{
    public override string Name => "Triple Mine";
    public override string Description => "Counts for Neighbouring Squares as ‘3’ mines";
    public override string Rarity => "UnCommon";

    public override Type GetMineType(){return typeof(TripleThreatMine);}
}
