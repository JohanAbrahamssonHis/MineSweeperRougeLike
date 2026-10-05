using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[CreateAssetMenu(fileName = "WhatAMine", menuName = "ScriptableObjects/Mine/WhatAMine", order = 22)]

public class SWhatAMine : SMine
{
    public override string Name => "What A Mine";
    public override string Description => "When Selected, becomes a random mine from the list of all mines in the game.";
    public override string Rarity => "Very Rare";

    public override Type GetMineType(){return typeof(WhatAMine);}

    public override void MinePicked()
    {
        base.MinePicked();
        MalwarePackage newMalwarePackage = Instantiate(new MalwarePackage());
        SMine selectedSMine = MineLibrary.Instance.GetRandomSMine();
        newMalwarePackage.mines.Add(Instantiate(selectedSMine));
        RunPlayerStats.Instance.AddMalwarePackage(newMalwarePackage);
        
        //Add new Mines
        MalwarePackage malwarePackage = RunPlayerStats.Instance.MalwarePackages.FirstOrDefault(x => x.mines.FirstOrDefault(y => y.GetType() == typeof(SWhatAMine)));

        //Remove this mine, to swap it with the new mine.
        if(malwarePackage != null)malwarePackage.RemoveMine(this);
        else Debug.LogError("Removed Mine was null");
        
        if (malwarePackage.mines.Count == 0)
        {
            RunPlayerStats.Instance.MalwarePackages.Remove(malwarePackage);
            Destroy(malwarePackage);
        }
    }
}
