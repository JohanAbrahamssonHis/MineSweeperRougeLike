using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[CreateAssetMenu(menuName = "Item/Goggles", fileName = "Goggles")]
public class Goggles : Item
{
    public override string Name => "Goggles";

    public override string Description => "Gain " + XRayCount + " Permantent XRays";

    public override string Rarity => "Rare";

    public XRay xRay;

    public int XRayCount = 2;

    public override void Function()
    {
        var EffectAbilties = RunPlayerStats.Instance.effectAbilities;    

        if(EffectAbilties.Any(x => x.GetType() == xRay.GetType()))
        {
           
            var existingXRay= EffectAbilties.Find(x => x.GetType() == xRay.GetType());

            existingXRay.baseCount += XRayCount;
            existingXRay.count += XRayCount;
            
            return;
        }

        XRay newXRay = Instantiate(xRay);
        newXRay.baseCount = XRayCount;
        newXRay.count = XRayCount;
        EffectAbilties.Add(newXRay);
    }

    public override void Join()
    {
        Function();
    }
}
