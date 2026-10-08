using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            ActionEvents.Instance.TriggerEventEffectAbility();
            ActionEvents.Instance.TriggerEventEffectAbilityActivated(this, squareMine);
            return;
        }
        if(count<=0)return;
        
        bool didWork = Function(squareMine);
        if(!didWork) return;

        if(tempCount>0)
        {
            tempCount--;
            count--;
        }
        else
        {
            count--;
        }
        ActionEvents.Instance.TriggerEventEffectAbility();
        ActionEvents.Instance.TriggerEventEffectAbilityActivated(this, squareMine);
    }
    
    public void ResetAbility()
    {
        count = baseCount+tempCount;
    }

    public void AddOrSetAbility(int baseCount, int tempCount)
    {
        var EffectAbilties = RunPlayerStats.Instance.effectAbilities;    

        if(EffectAbilties.Any(x => x.GetType() == this.GetType()))
        {
            var existingEffect = EffectAbilties.Find(x => x.GetType() == this.GetType());

            existingEffect.tempCount += tempCount;
            existingEffect.baseCount += baseCount;
            existingEffect.count = existingEffect.tempCount+existingEffect.baseCount;
            return;
        }

        EffectAbility newEffectAbility = Instantiate(this);
        newEffectAbility.tempCount = tempCount;
        newEffectAbility.baseCount = baseCount;
        newEffectAbility.count = newEffectAbility.tempCount+newEffectAbility.baseCount;
        EffectAbilties.Add(newEffectAbility);
    }

    public abstract bool Function(SquareMine squareMine);
    public abstract string Name { get; }
    public abstract string Description { get; }
}
