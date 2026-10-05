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
        var EffectAbilties = RunPlayerStats.Instance.effectAbilities;    

        if(EffectAbilties.Any(x => x.GetType() == sensor.GetType()))
        {
            var existingSensor = EffectAbilties.Find(x => x.GetType() == sensor.GetType());

            existingSensor.tempCount += tempCount;
            existingSensor.count += tempCount;
            return;
        }

        Sensor newSensor = Instantiate(sensor);
        newSensor.tempCount = tempCount;
        newSensor.count = tempCount;
        newSensor.baseCount = 0;
        EffectAbilties.Add(newSensor);
    }

    public override void Join()
    {
        Function();
    }
}
