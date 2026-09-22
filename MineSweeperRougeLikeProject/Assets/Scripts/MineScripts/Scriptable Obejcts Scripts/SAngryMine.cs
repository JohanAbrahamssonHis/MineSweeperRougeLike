using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AngryMine", menuName = "ScriptableObjects/Mine/AngryMine", order = 5)]
public class SAngryMine : SMine
{
    public override string Name => "Angry Mine";
    public override string Description => "Counts for Neighbouring Squares as -2 mines";
    public override string Rarity => "Rare";

    public override Type GetMineType(){return typeof(AngryMine);}
}
