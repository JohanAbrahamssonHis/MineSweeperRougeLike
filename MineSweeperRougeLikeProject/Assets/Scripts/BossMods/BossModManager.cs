using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class BossModManager : MonoBehaviour
{
    public BossModification _bossModification;

    public Grid grid;
    
    public void Start()
    {
        List<BossModification> bossModifications = BossModificationLibrary.Instance.bossModifications;

        _bossModification = Instantiate(bossModifications[Random.Range(0,bossModifications.Count)]);

        RunPlayerStats.Instance.BossModification = _bossModification;
        
        _bossModification.JoinModification();
        _bossModification.Modification();
    }

    public void Update()
    {
        _bossModification.UpdateModification();
    }
}
