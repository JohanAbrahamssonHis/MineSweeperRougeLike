using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MushroomMine : Mine
{
    public int currentTick;
    public int tickMax;

    public override void GlobalMineSubscribe()
    {
        base.MineSubscribe();
        ActionEvents.Instance.OnLeaveRoom += MushroomTick;
    }

    public override void GlobalMineUnSubscribe()
    {
        base.MineUnSubscribe();
        ActionEvents.Instance.OnLeaveRoom -= MushroomTick;
    }

    public void MushroomTick()
    {
        currentTick++;
        if(currentTick<tickMax) return;
        
        MalwarePackage MushroomMalwarePackage = RunPlayerStats.Instance.MalwarePackages.First(x => x.mines.First(y => y.GetMineType() == typeof(MushroomMine)));
        MushroomMalwarePackage.RemoveMine(Context.MineData);
    }

    public override void Activate()
    {
        base.Activate();
        MalwarePackage MushroomMalwarePackage = RunPlayerStats.Instance.MalwarePackages.First(x => x.mines.First(y => y.GetMineType() == typeof(MushroomMine)));
        MushroomMalwarePackage.AddMine(Instantiate(Context.MineData));
    }
}
