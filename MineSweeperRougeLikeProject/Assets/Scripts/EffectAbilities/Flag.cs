using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "EffectAbility/Flag", fileName = "Flag")]
public class Flag : EffectAbility
{
    public Flag()
    {
        isInfinite = true;
    }

    protected override void Function(SquareMine squareMine)
    {
        if (squareMine.squareRevealed || RunPlayerStats.Instance.EndState) return;
        squareMine.hasFlag = !squareMine.hasFlag;

        if (RunPlayerStats.Instance.DebugMode)
        {
            return;
        }

        ActionEvents.Instance.TriggerEventFlag();
        SoundManager.Instance.Play("Flag", squareMine.transform, true, 1);
    }

    public override string Name => "Flag";
    public override string Description => "Prevents actions or effects on that tile";
}
