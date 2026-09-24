using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "LemonMine", menuName = "ScriptableObjects/Mine/LemonMine", order = 8)]
public class SLemonMine : SMine
{
    
    public override string Name => "Lemon Mine";
    public override string Description => "Whenever you gain health, lose 1$";
    public override string Rarity => "UnCommon";

    public override Type GetMineType(){return typeof(LemonMine);}
}
