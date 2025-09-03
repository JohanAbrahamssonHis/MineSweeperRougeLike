using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public int cost;
    public Sprite sprite;
    public int rarity;
    public abstract void Function();
    public abstract void Join();

    public virtual void Unsubscribe(){}
}
