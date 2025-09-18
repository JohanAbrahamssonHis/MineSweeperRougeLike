using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
[CreateAssetMenu(menuName = "BossMod/Freeze Dry", fileName = "Freeze Dry")]
public class FreezeDry : BossModification
{
    public float timeMultiplierBase = 2;
    private float _timeMultiplier;
    public float minimumHeatTrigger = 0;
    private bool _fasterTime;
    public override void Modification() { }

    public override void UpdateModification()
    {
        if(!RunPlayerStats.Instance.MineRoomManager.AfterFirstMove) return;
        
        if (RunPlayerStats.Instance.Heat <= minimumHeatTrigger & !_fasterTime)
        {
            //Faster Time
            RunPlayerStats.Instance.TimeMult *= timeMultiplierBase;
            _fasterTime = true;
        }

        if (!(RunPlayerStats.Instance.Heat > minimumHeatTrigger & _fasterTime)) return;
        //Reset Time
        RunPlayerStats.Instance.TimeMult *= 1 / timeMultiplierBase;
        _fasterTime = false;
    }

    public override void UnsubscribeModification()
    {
        if (!_fasterTime) return;
        _timeMultiplier *= 1/timeMultiplierBase;
        RunPlayerStats.Instance.TimeMult *= _timeMultiplier;
    }
}
