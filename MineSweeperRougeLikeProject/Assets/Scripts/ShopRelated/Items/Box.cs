using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Item/Box", fileName = "Box")]
public class Box : Item
{
    public int maxMoney;
    public int minMoney;

    public int minesAddedOdds;
    public int maxMines;
    public int minMines;
    
    public override void Function()
    {
        int randomAmountOfMoney = Random.Range(minMoney, maxMoney+1);
        int randomAmountOfMines = Random.Range(0,minesAddedOdds)==0 ? Random.Range(minMines, maxMines+1) : 0;

        RunPlayerStats.Instance.Money += randomAmountOfMoney;

        MalwarePackage selectedMalwarePackage =
            RunPlayerStats.Instance.MalwarePackages[Random.Range(0, RunPlayerStats.Instance.MalwarePackages.Count)];
        
        for (int i = 0; i < randomAmountOfMines; i++)
        {
            Mine selectedMine = Instantiate(selectedMalwarePackage.mines[Random.Range(0, selectedMalwarePackage.mines.Count)]);
           selectedMalwarePackage.AddMine(selectedMine);
        }
    }

    public override void Join()
    {
        Function();
    }
}
