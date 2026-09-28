using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "SleepyMine", menuName = "ScriptableObjects/Mine/SleepyMine", order = 12)]
public class SSleepyMine : SMine
{
    public override string Name => "Sleepy Mine";
    public override string Description => "When activated, lose 15s";
    public override string Rarity => "Uncommon";

    public override Type GetMineType(){return typeof(SleepyMine);}
}
