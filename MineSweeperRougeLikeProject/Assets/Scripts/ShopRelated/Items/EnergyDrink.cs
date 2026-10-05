using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/EnergyDrink", fileName = "EnergyDrink")]
public class EnergyDrink : Item
{
    public override string Name => "Energy Drink";

    public override string Description => "Gain 7$ but lose 1 point per action";

    public override string Rarity => "Rare";

    public override void Function()
    {
        RunPlayerStats.Instance.PointsGain -= 1;
        RunPlayerStats.Instance.Money += 7;
    }

    public override void Join()
    {
        Function();
    }
}
