using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NormalMine", menuName = "ScriptableObjects/Mine/NormalMine", order = 0)]
public class NormalMine : SMine
{
    public override string Name => "Normal Mine";
    public override string Description => "A Standard Mine";
    public override string Rarity => "Common";
}
