using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Singletons/StartData", fileName = "StartData")]
public class StartData : ScriptableObject
{
    private static StartData _instance;
    
    public static StartData Instance
    {
        get
        {
            if (_instance == null) _instance = Resources.Load<StartData>("Singletons/StartData");
            return _instance;
        }
         
    }

    public void StartValues(RunPlayerStats rPS)
    {
        rPS.HealthDamageModifier = 0;
        rPS.HealthDamageMultModifier = 1;
        rPS.Health = 5;
        rPS.Time = 4*60;
        rPS.TimeMult = 1;
        rPS.TimeGain = 15;
        rPS.Money = 5;
        rPS.MoneyGain = 1;
        rPS.Points = 0;
        rPS.PointsGain = 10;
        rPS.Heat = 0;
        rPS.HeatGain = 0.15f;
        rPS.FloorCount = 1;
        rPS.RoomCountCleared = 0;
        rPS.EliteRoomCount = 1;
        rPS.RoomCount = 2;
        rPS.ShopCount = 2;
        rPS.RoomLock = 2;
        rPS.GridSize = new Vector2(6, 6);
        rPS.ActiveTimer = false;
        rPS.MalwarePackages = new List<MalwarePackage>();
        rPS.MineRoomManager = null;
        rPS.FloorManager = null;
        rPS.FlagMineSelected = null;
        rPS.Inventory = new List<Item>();
        rPS.BossModification = null;
        rPS.BannedBossModifications = new List<string>();
        rPS.mineVisualizer = null;
        SetEffectAbilities(rPS);
    }

    private void SetEffectAbilities(RunPlayerStats rPS)
    {
        rPS.effectAbilities.Clear();
        //startEffectAbilities.ForEach(x => effectAbilities.Add(Instantiate(x)));
        rPS.currentEffectAbility = rPS.effectAbilities.First();
    }
}
