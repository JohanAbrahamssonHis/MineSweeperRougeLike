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
        
        MalwarePackage MushroomMalwarePackage = Instantiate(new MalwarePackage());
        MushroomMalwarePackage.RemoveMine(Context.MineData);
        RunPlayerStats.Instance.AddMalwarePackage(MushroomMalwarePackage);
    }

    public override void Activate()
    {
        base.Activate();
        MalwarePackage MushroomMalwarePackage = Instantiate(new MalwarePackage());
        MushroomMalwarePackage.AddMine(Instantiate(Context.MineData));
        RunPlayerStats.Instance.AddMalwarePackage(MushroomMalwarePackage);
    }
}
