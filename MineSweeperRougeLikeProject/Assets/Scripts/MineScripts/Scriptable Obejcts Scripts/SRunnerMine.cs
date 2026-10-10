using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "RunnerMine", menuName = "ScriptableObjects/Mine/RunnerMine", order = 16)]
public class SRunnerMine : SMine, ITimeObject
{
    public override string Name => "Runner Mine";
    public override string Description => "Every " + runTime + "s, Move to another neighbour square";
    public override string Rarity => "UnCommon";

    public override Type GetMineType(){return typeof(RunnerMine);}

    public float runTime = 15;

    public override void SendDataToMine(Mine mine){(mine as RunnerMine).runTime = runTime;}
}
