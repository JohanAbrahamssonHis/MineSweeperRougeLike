using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMineHolder : MonoBehaviour
{
    public MalwarePackage malwarePackage;

    public void Addmines()
    {
        RunPlayerStats.Instance.AddMalwarePackage(malwarePackage);
    }
}
