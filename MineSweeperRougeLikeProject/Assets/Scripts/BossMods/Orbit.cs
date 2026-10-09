using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[CreateAssetMenu(menuName = "BossMod/Orbit", fileName = "Orbit")]
public class Orbit : BossModification
{
    public override string Description => "The Mouse will orbit its normal position";

    private Vector2 offSet;

    public float frequency = 1;
    public float amplitude = 1;

    public override void Modification()
    {
        
    }

    public override void UpdateModification()
    {
        base.UpdateModification();
        InputHandler inputHandler = RunPlayerStats.Instance.InputHandler;
        offSet = new Vector2(Mathf.Sin(Time.time*frequency)*amplitude, Mathf.Cos(Time.time*frequency)*amplitude);
        inputHandler.MousePositionOffset = offSet;
        inputHandler.SetFakeCursorPosition(offSet);
    }

    public override void UnsubscribeModification()
    {
        base.UnsubscribeModification();
        RunPlayerStats.Instance.InputHandler.MousePositionOffset = Vector2.zero;
        RunPlayerStats.Instance.InputHandler.SetFakeCursorPosition(Vector2.zero); 
    }
}
