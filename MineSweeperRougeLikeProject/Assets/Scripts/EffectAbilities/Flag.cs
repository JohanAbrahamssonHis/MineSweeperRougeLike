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

    public override bool Function(SquareMine squareMine)
    {
        if (squareMine.squareRevealed || RunPlayerStats.Instance.EndState) return false;
        squareMine.hasFlag = !squareMine.hasFlag;
        squareMine.SetFlagSprite();

        if (RunPlayerStats.Instance.DebugMode)
        {
            return false;
        }

        ActionEvents.Instance.TriggerEventFlag();
        SoundManager.Instance.Play("Flag", squareMine.transform, true, 1);
        return true;
    }

    public override string Name => "Flag";
    public override string Description => "Prevents actions or effects on that tile";
}
