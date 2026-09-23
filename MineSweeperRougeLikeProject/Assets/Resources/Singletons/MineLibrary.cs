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

    [SerializeField] private List<SMine> _baseSetSMines;

    public List<SMine> SMines { private set; get; }

    public void ConnectLibrary() => SMines = new(_baseSetSMines);

    public void SetListDependancy(List<SMine> SMines) => this.SMines = SMines;

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
