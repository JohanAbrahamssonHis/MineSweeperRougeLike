using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[CreateAssetMenu(menuName = "BossMod/Spin", fileName = "Spin")]
public class Spin : BossModification
{
    public float Speed;
    private Grid grid;
    
    public override void UpdateModification()
    {
        grid.transform.RotateAround(Vector2.zero, Vector3.forward, Time.deltaTime*Speed);
    }

    public override void Modification()
    {
        grid = RunPlayerStats.Instance.MineRoomManager.grid;
    }
}
