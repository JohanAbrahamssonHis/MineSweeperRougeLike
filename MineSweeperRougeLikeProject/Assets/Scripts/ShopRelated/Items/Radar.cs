using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/Radar", fileName = "Radar")]
public class Radar : Item
{
    public override string Name => "Radar";

    public override string Description => "Gain one permanent Sensor";

    public override string Rarity => "Uncommon";

    public Sensor sensor;

    public override void Function()
    {
        sensor.AddOrSetAbility(1,0);
    }

    public override void Join()
    {
        Function();
    }
}
