using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NegativeMine", menuName = "ScriptableObjects/Mine/NegativeMine", order = 4)]
public class SNegativeMine : SMine
{
    public override string Name => "Sad Mine";
    public override string Description => "Counts for Neighbouring Squares as ‘-1’ mines";
    public override string Rarity => "UnCommon";

    public override Type GetMineType(){return typeof(SadMine);}
}
