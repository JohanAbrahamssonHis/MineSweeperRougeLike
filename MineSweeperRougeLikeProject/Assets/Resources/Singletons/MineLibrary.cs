using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Singletons/Library/MineLibrary", fileName = "MineLibrary")]
public class MineLibrary : ScriptableObject
{
    private static MineLibrary _instance;

    public static MineLibrary Instance
    {
        get
        {
            if (_instance == null) _instance = Resources.Load<MineLibrary>("Singletons/MineLibrary");
            return _instance;
        }
    }

    public List<SMine> SMines;

    public SMine GetRandomSMine()
    {
        if (SMines.Count == 0)
        {
            Debug.LogWarning("No SMines available.");
            return null;
        }

        int randomIndex = UnityEngine.Random.Range(0, SMines.Count);
        return SMines[randomIndex];
    }

    public bool UseSetSMine = false;
    public SMine SetSMine;
}
