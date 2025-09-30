using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectAbility : ScriptableObject, ITextable
{
    public bool isInfinite;
    public int baseCount = 1;
    public int count = 1;
    public Sprite sprite;
    
    
    public void CallAbility(SquareMine squareMine)
    {
        if (isInfinite)
        {
            Function(squareMine);
            return;
        }
        if(count<=0)return;
        count--;
        Function(squareMine);
    }
    
    public void ResetAbility()
    {
        count = baseCount;
    }

    protected abstract void Function(SquareMine squareMine);
    public abstract string Name { get; }
    public abstract string Description { get; }
}
