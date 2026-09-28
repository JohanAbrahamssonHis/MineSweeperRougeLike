using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "PirateMine", menuName = "ScriptableObjects/Mine/PirateMine", order = 13)]

public class SPirateMine : SMine
{
    
    public override string Name => "Pirate Mine";
    public override string Description => "Deals damage equal to every 5$ you have";
    public override string Rarity => "Uncommon";

    public override Type GetMineType(){return typeof(PirateMine);}
}
