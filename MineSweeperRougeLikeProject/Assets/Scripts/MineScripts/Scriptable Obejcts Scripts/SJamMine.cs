using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "JamMine", menuName = "ScriptableObjects/Mine/JamMine", order = 10)]
public class SJamMine : SMine
{
    public override string Name => "Jam Mine";

    public override string Description => "Neighbouring squares only show question mark";

    public override string Rarity => "Very Rare";

    public override Type GetMineType() {return typeof(JamMine);}

    public override void SendDataToMine(Mine mine){(mine as JamMine).questionMarkSprite = QuestionMarkSprite;}

    public Sprite QuestionMarkSprite;
}
