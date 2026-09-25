using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ProximityMine", menuName = "ScriptableObjects/Mine/ProximityMine", order = 10)]
public class SProximityMine : SMine
{
    public override string Name => "Proximity Mine";
    public override string Description => "When a neighbour is revealed, this is shown and will explode shortly. Do NOT have your mouse close then";
    public override string Rarity => "Odd";

    [SerializeField] private float duration = 3f;
    [SerializeField] private float distance = 2f;

    [SerializeField] private Sprite targetSprite;

    public override Type GetMineType(){return typeof(ProximityMine);}

    public override void SendDataToMine(Mine mine){
        (mine as ProximityMine).duration = duration;
        (mine as ProximityMine).distance = distance;
        (mine as ProximityMine).targetSprite = targetSprite;
    }
}
