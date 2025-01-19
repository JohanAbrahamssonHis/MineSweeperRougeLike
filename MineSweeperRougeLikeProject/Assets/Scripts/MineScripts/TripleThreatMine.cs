using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleThreatMine : Mine
{
    // Start is called before the first frame update
    public override void SetUpMine()
    {
        base.SetUpMine();
        weight = 3;
        SetStandardNeighbours(neighbours);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
