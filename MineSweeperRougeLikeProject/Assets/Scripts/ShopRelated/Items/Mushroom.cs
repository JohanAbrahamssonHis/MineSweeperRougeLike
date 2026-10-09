using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/Mushroom", fileName = "Mushroom")]
public class Mushroom : Item
{
    public int pointsGain = 1;
    public int healthGain = 1;
    public float timeGain = 1;

    public override string Name => "Mushroom";

    public override string Description => "Gain "+pointsGain+" point per action, Gain "+healthGain+" Health, Gain "+timeGain+" minute and 1 Mushroom Mine.";

    public override string Rarity => "Rare";

    public override void Function()
    {
        RunPlayerStats.Instance.PointsGain += 1;
        RunPlayerStats.Instance.Health += 1;
        RunPlayerStats.Instance.TimeGain += 60;

        MalwarePackage newMalwarePackage = Instantiate(new MalwarePackage());
        SMushroomMine sMushroomMine = Instantiate(MineLibrary.Instance.GetSelectedSMineByType(typeof(SMushroomMine))) as SMushroomMine;
        newMalwarePackage.AddMine(sMushroomMine);
        RunPlayerStats.Instance.AddMalwarePackage(newMalwarePackage);
    }

    public override void Join()
    {
        Function();
    }
}
