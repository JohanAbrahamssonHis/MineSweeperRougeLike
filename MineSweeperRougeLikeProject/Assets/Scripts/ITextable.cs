using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITextable
{
    string Name { get; }
    string Description { get; }
    string Rarity => "";
}
