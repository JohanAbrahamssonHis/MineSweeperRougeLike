using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "ImpatientMine", menuName = "ScriptableObjects/Mine/ImpatientMine", order = 13)]

public class SImpatientMine : SMine, ITimeObject
{
    
    public override string Name => "Impatient Mine";
    public override string Description => "After the first action, if no other actions have been done for "+ explodeTime +"s, explode";
    public override string Rarity => "Rare";

    public override Type GetMineType(){return typeof(ImpatientMine);}

    public float explodeTime = 30;

    public override void SendDataToMine(Mine mine){(mine as ImpatientMine).explodeTime = explodeTime;}

}
