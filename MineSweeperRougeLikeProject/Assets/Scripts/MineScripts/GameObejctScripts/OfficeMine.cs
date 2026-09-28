using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeMine : Mine
{
    public int odds;
    public override void Activate()
    {
        if(Random.Range(0,odds)==0) base.Activate();
    }
}
