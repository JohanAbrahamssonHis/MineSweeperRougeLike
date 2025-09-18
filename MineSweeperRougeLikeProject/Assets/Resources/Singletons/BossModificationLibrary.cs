using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Singletons/BossModificationLibrary", fileName = "BossModificationLibrary")]
public class BossModificationLibrary : ScriptableObject
{
    private static BossModificationLibrary _instance;

    public static BossModificationLibrary Instance
    {
        get
        {
            if (_instance == null) _instance = Resources.Load<BossModificationLibrary>("Singletons/BossModificationLibrary");
            return _instance;
        }
    }

    public List<BossModification> bossModifications;
}
