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
        var EffectAbilties = RunPlayerStats.Instance.effectAbilities;    

        if(EffectAbilties.Any(x => x.GetType() == sensor.GetType()))
        {
           
            var existingSensor = EffectAbilties.Find(x => x.GetType() == sensor.GetType());

            existingSensor.baseCount++;
            existingSensor.count++;
            
            return;
        }
        EffectAbilties.Add(Instantiate(sensor));
    }

    public override void Join()
    {
        Function();
    }
}
