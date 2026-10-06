using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
[CreateAssetMenu(menuName = "Item/Radio", fileName = "Radio")]
public class Radio : Item
{
    public override string Name => "Radio";

    public override string Description => "Gain 3 temporary Sensor abilities";

    public override string Rarity => "Common";

    public Sensor sensor;
    public int tempCount = 3;

    public override void Function()
    {
        sensor.AddOrSetAbility(0,tempCount);
    }

    public override void Join()
    {
        Function();
    }
}
