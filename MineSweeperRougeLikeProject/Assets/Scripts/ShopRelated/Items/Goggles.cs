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
        xRay.AddOrSetAbility(XRayCount,0);
    }

    public override void Join()
    {
        Function();
    }
}
