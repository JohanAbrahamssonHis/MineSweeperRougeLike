using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AgentMine", menuName = "ScriptableObjects/Mine/AgentMine", order = 9)]
public class SAgentMine : SMine
{

    public override Type GetMineType() { return typeof(AgentMine);}
    
    public override string Name => "Agent Mine";
    public override string Description => "After each action, moves to a Neighbouring Square";
    public override string Rarity => "Common";

}