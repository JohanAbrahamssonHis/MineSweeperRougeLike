using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[CreateAssetMenu(menuName = "BossMod/Spin", fileName = "Spin")]
public class Spin : BossModification<Grid>
{
    public float Speed;
    
    public override void UpdateModification(Grid value)
    {
        value.transform.RotateAround(Vector2.zero, Vector3.forward, Time.deltaTime*Speed);
    }

    public override void Modification(Grid value)
    {
        
    }
}
