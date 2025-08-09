using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public int cost;
    public Sprite sprite;
    public abstract void Function();
    public abstract void Join();
}
