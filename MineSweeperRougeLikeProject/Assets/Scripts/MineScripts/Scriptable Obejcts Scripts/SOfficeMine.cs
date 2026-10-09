using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "OfficeMine", menuName = "ScriptableObjects/Mine/OfficeMine", order = 14)]

public class SOfficeMine : SMine
{

    public override string Name => "Office Mine";
    public override string Description => "When activated, "+ 100/odds +"% odds to do nothing";
    public override string Rarity => "Rare";

    public override Type GetMineType(){return typeof(OfficeMine);}

    public int odds;

    public override void SendDataToMine(Mine mine)
    {
        (mine as OfficeMine).odds = odds;
    }
}
