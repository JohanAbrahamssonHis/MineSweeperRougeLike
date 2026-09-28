using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "MOABMine", menuName = "ScriptableObjects/Mine/MOABMine", order = 12)]
public class SMOABMine : SMine
{
    public override string Name => "MOAB Mine";
    public override string Description => "Every 3 actions, add a random temporary mine";
    public override string Rarity => "Rare";

    public override Type GetMineType(){return typeof(MOABMine);}

    public SMine child;
    public int amountOfActions;

    public override void SendDataToMine(Mine mine){
        (mine as MOABMine).childMineData = child;
        (mine as MOABMine).amountOfActionsBeforeMother = amountOfActions;    
    }
}
