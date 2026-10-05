using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectAbility : ScriptableObject, ITextable
{
    public bool isInfinite;
    public int baseCount = 1;
    public int count = 1;
    public int tempCount = 0;
    public Sprite sprite;
    
    
    public void CallAbility(SquareMine squareMine)
    {
        if (isInfinite)
        {
            Function(squareMine);
            return;
        }
        if(count<=0)return;
        //TODO: Add if the effect fails to trigger, it should not count as a use of the ability
        if(tempCount>0)
        {
            tempCount--;
            count--;
        }
        else
        {
            count--;
        }
        Function(squareMine);
    }
    
    public void ResetAbility()
    {
        count = baseCount+tempCount;
    }

    protected abstract void Function(SquareMine squareMine);
    public abstract string Name { get; }
    public abstract string Description { get; }
}
