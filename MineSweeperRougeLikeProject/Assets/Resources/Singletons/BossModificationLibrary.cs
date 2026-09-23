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

    [SerializeField] private List<BossModification> _baseSetBossModifications;

    public List<BossModification> BossModifications { private set; get; }

    public void ConnectLibrary() => BossModifications = new(_baseSetBossModifications);

    public void SetListDependancy(List<BossModification> BossModifications) => this.BossModifications = BossModifications;


    public BossModification GetRandomBossModification()
    {
        if (BossModifications.Count == 0)
        {
            Debug.LogWarning("No boss modifications available.");
            return null;
        }

        int randomIndex = UnityEngine.Random.Range(0, BossModifications.Count);
        return BossModifications[randomIndex];
    }

    public bool UseSetBossModification = false;
    public BossModification SetBossModification;
}
