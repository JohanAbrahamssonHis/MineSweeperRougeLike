using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "PressureMine", menuName = "ScriptableObjects/Mine/PressureMine", order = 18)]
public class SPressureMine : SMine
{
    public override string Name => "Pressure Mine";
    public override string Description => "After each room, this mine gains 1 damage. If activated, resets its damage. Cannot kill you unless you have 1 heatlh";
    public override string Rarity => "Rare";

    public override Type GetMineType(){return typeof(PressureMine);}
}
