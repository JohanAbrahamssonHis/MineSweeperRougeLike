using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "SleepyMine", menuName = "ScriptableObjects/Mine/SleepyMine", order = 12)]
public class SSleepyMine : SMine, ITimeObject
{
    public override string Name => "Sleepy Mine";
    public override string Description => "When activated, lose "+ timeLost +"s";
    public override string Rarity => "Uncommon";

    public float timeLost = 15;

    public override Type GetMineType(){return typeof(SleepyMine);}

    public override void SendDataToMine(Mine mine){
    (mine as SleepyMine).timeLost = timeLost;
    }
}
