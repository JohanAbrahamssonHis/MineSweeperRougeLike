using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "PirateMine", menuName = "ScriptableObjects/Mine/PirateMine", order = 13)]

public class SPirateMine : SMine
{
    
    public override string Name => "Pirate Mine";
    public override string Description => "Deals damage equal to every " + DamagePerMoney + "$ you have";
    public override string Rarity => "Uncommon";

    public int DamagePerMoney = 5;

    public override Type GetMineType(){return typeof(PirateMine);}

    public override void SendDataToMine(Mine mine){
    (mine as PirateMine).DamagePerMoney = DamagePerMoney;
    }
}
