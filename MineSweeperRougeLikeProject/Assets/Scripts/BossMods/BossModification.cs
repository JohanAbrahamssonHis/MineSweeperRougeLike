using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossModification : ScriptableObject, ITextable
{
    public new string name;
    public Sprite sprite;
    
    public abstract void Modification();

    public virtual void UpdateModification() { }

    public virtual void JoinModification() { }
    
    public virtual void UnsubscribeModification() { }
    public string Name => name;
    public abstract string Description { get; }
}
