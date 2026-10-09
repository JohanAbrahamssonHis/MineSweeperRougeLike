using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/EnergyDrink", fileName = "EnergyDrink")]
public class EnergyDrink : Item
{
    public int moneyGain = 7;
    public int pointsLoss = 1;


    public override string Name => "Energy Drink";

    public override string Description => "Gain "+moneyGain+"$ but lose "+pointsLoss+" point per action";

    public override string Rarity => "Rare";

    public override void Function()
    {
        RunPlayerStats.Instance.PointsGain -= pointsLoss;
        RunPlayerStats.Instance.Money += moneyGain;
    }

    public override void Join()
    {
        Function();
    }
}
