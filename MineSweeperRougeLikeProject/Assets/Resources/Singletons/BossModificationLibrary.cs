using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Singletons/Library/BossModificationLibrary", fileName = "BossModificationLibrary")]
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

    public BossModification GetRandomBossModification()
    {
        if (bossModifications.Count == 0)
        {
            Debug.LogWarning("No boss modifications available.");
            return null;
        }

        int randomIndex = UnityEngine.Random.Range(0, bossModifications.Count);
        return bossModifications[randomIndex];
    }

    public bool UseSetBossModification = false;
    public BossModification SetBossModification;
}
