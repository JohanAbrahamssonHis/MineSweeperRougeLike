using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject, ITextable
{
    public int cost;
    public Sprite sprite;
    public int rarity;
    public abstract void Function();
    public abstract void Join();

    public virtual void Unsubscribe(){}
    public abstract string Name { get; }
    public abstract string Description { get; }
    
    public abstract string Rarity { get; }
}
