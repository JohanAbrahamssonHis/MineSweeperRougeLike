using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossModification<T> : ScriptableObject
{
    public new string name;

    public T t;
    
    public abstract void Modification(T value);

    public virtual void UpdateModification(T value)
    {
        
    }

    public virtual void JoinModification(T value)
    {
        
    }
    
    public virtual void UnsubscribeModification(T value)
    {
        
    }

    public Type GetGenericType()
    {
        return typeof(T);
    }
}
