using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TimeBombMine", menuName = "ScriptableObjects/Mine/TimeBombMine", order = 9)]
public class STimeBombMine : SMine, ITimeObject
{
    public override string Name => "Time Bomb Mine";

    public override string Description => "If activated, lose half your time";

    public override string Rarity => "Rare";

    public override Type GetMineType() {return typeof(TimeBombMine);}
}
