using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoubleMine", menuName = "ScriptableObjects/Mine/DoubleMine", order = 1)]
public class SDoubleMine : SMine
{
    public override string Name => "Double Mine";
    public override string Description => "Counts for Neighbouring Squares as ‘2’ mines";
    public override string Rarity => "Common";

    public override Type GetMineType(){return typeof(DoubleTroubleMine);}
}
