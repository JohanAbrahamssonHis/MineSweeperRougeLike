using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "BossMod/DanceAroundTheRoses", fileName = "DanceAroundTheRoses")]
public class DanceAroundTheRoses : BossModification
{
    public override string Description => "All content in the squares will dance around";

    [SerializeField]
    private float Amplitude = 3.0f;

    [SerializeField]
    private float Frequency = 1.0f;

    public override void Modification()
    {
        
    }

    public override void UpdateModification()
    {
        foreach (var square in RunPlayerStats.Instance.MineRoomManager.grid.squares)
        {
            if (square.squareRevealed)
            {
                //The squares should rotate by a sin wave and end up at a equal length both times.
                float angle = Mathf.Sin(Time.time * Frequency) * Amplitude;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                Quaternion startRot = square.GetContainer().transform.localRotation;
                square.GetContainer().transform.localRotation = startRot * rotation;
            }
        }


        //.RotateAround(Vector2.zero, Vector3.forward, Time.deltaTime*10);
    }
}
