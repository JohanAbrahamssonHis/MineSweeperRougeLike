using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "BossMod/DanceAroundTheRoses", fileName = "DanceAroundTheRoses")]
public class DanceAroundTheRoses : BossModification
{
    public override string Description => "All content in the squares will dance around";

    [SerializeField]
    private float Amplitude = 10.0f;

    [SerializeField]
    private float Frequency = 4.0f;

    [SerializeField]
    private float Distance = 0.4f;

    public override void Modification()
    {
        
    }

    public override void UpdateModification()
    {
        foreach (var square in RunPlayerStats.Instance.MineRoomManager.grid.squares)
        {
            if (square.squareRevealed)
            {
                float angle = Mathf.Sin(Time.time * Frequency) * Amplitude;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                square.GetContainer().transform.localRotation = rotation;

                
                square.GetContainer().transform.localPosition = Vector2.Lerp(new Vector2(-Distance,0), new Vector2(Distance,0), (Mathf.Sin(Time.time)+1)/2);
            }
        }
    }
}
