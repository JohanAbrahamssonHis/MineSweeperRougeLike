using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WhatAMine : Mine
{
    public override void GlobalMineSubscribe()
    {
        base.GlobalMineSubscribe();
        ActionEvents.Instance.OnStartRun += SetWhatAMine;
    }

    public override void GlobalMineUnSubscribe()
    {
        base.GlobalMineSubscribe();
        ActionEvents.Instance.OnStartRun -= SetWhatAMine;
    }

    public void SetWhatAMine()
    {
        MalwarePackage newMalwarePackage = Instantiate(new MalwarePackage());
        SMine selectedSMine = MineLibrary.Instance.GetRandomSMine();
        newMalwarePackage.mines.Add(Instantiate(selectedSMine));
        RunPlayerStats.Instance.AddMalwarePackage(newMalwarePackage);
        
        //Add new Mines
        MalwarePackage malwarePackage = RunPlayerStats.Instance.MalwarePackages.FirstOrDefault(x => x.mines.FirstOrDefault(y => y.GetType() == typeof(SWhatAMine)));

        //Remove this mine, to swap it with the new mine.
        if(malwarePackage != null)malwarePackage.RemoveMine(MineData);
        else Debug.LogError("Removed Mine was null");
        
        if (malwarePackage.mines.Count == 0)
        {
            RunPlayerStats.Instance.MalwarePackages.Remove(malwarePackage);
            Destroy(malwarePackage);
        }
    }

}
