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
    public override void Modification()
    {
        
    }

    public override void UpdateModification()
    {
        if (RunPlayerStats.Instance.Heat <= minimumHeatTrigger &&
            RunPlayerStats.Instance.MineRoomManager.AfterFirstMove && !_fasterTime)
        {
            //Faster Time
            _timeMultiplier = timeMultiplierBase;
            RunPlayerStats.Instance.TimeMult *= _timeMultiplier;
            _fasterTime = true;
        }

        if (!(RunPlayerStats.Instance.Heat <= minimumHeatTrigger) || !_fasterTime) return;
        //Reset Time
        _timeMultiplier *= 1/timeMultiplierBase;
        RunPlayerStats.Instance.TimeMult *= _timeMultiplier;
        _fasterTime = false;
    }

    public override void UnsubscribeModification()
    {
        if (!_fasterTime) return;
        _timeMultiplier *= 1/timeMultiplierBase;
        RunPlayerStats.Instance.TimeMult *= _timeMultiplier;
    }
}
