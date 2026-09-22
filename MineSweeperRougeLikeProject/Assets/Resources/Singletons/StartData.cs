using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Singletons/StartData", fileName = "StartData")]
public class StartData : ScriptableObject
{
    #region SetUpBasics
    [Header("SetUpBasics")]
    [SerializeField] private GameObject MainComponents = null;
    #endregion

    #region Health
    [Header("Health")]
    [SerializeField] private  int HealthDamageModifier = 0;
    [SerializeField] private  int HealthDamageMultModifier = 1;
    [SerializeField] private  int Health = 5;
    #endregion

    #region Time
    [Header("Time")]
    [SerializeField] private  float Time = 4*60;
    [SerializeField] private  float TimeMult = 1;
    [SerializeField] private  float TimeGain = 15;
    #endregion

    #region Money
    [Header("Money")]
    [SerializeField] private  int Money = 5;
    [SerializeField] private  int MoneyGain = 1;
    #endregion

    #region Heat
    [Header("Heat")]
    [SerializeField] private  float Heat = 0;
    [SerializeField] private  float HeatGain = 0.15f;
    #endregion

    #region Point
    [Header("Point")]
    [SerializeField] private  int Points = 0;
    [SerializeField] private  int PointsGain = 10;
    #endregion

    #region Room and Floor
    [Header("Room and Floor")]
    [SerializeField] private  int FloorCount = 1;
    [SerializeField] private  int RoomCountCleared = 0;
    [SerializeField] private  int EliteRoomCount = 1;
    [SerializeField] private  int RoomCount = 2;
    [SerializeField] private  int ShopCount = 2;
    [SerializeField] private  int RoomLock = 2;
    #endregion

    #region General
    [Header("General")]
    [SerializeField] private  Vector2 GridSize = new(6, 6);
    [SerializeField] private  bool ActiveTimer = false;

    [SerializeField] private  SMine FlagMineSelected = null;
    #endregion

    #region Managers
    [Header("Managers")]
    [SerializeField] private  List<MalwarePackage> MalwarePackages = new List<MalwarePackage>();
    [SerializeField] private  MineRoomManager MineRoomManager = null;
    [SerializeField] private  FloorManager FloorManager = null;
    [SerializeField] private  List<Item> Inventory = new List<Item>();
    [SerializeField] private  BossModification BossModification = null;
    [SerializeField] private  List<string> BannedBossModifications = new List<string>();
    [SerializeField] private List<EffectAbility> startEffectAbilities;
    #endregion

    #region Visualizers
    [Header("Visualizers")]
    [SerializeField] private MineViusalizer mineVisualizer = null;
    #endregion


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
        rPS.HealthDamageModifier = HealthDamageModifier;
        rPS.HealthDamageMultModifier = HealthDamageMultModifier;
        rPS.Health = Health;
        rPS.Time = Time;
        rPS.TimeMult = TimeMult;
        rPS.TimeGain = TimeGain;
        rPS.Money = Money;
        rPS.MoneyGain = MoneyGain;
        rPS.Points = Points;
        rPS.PointsGain = PointsGain;
        rPS.Heat = Heat;
        rPS.HeatGain = HeatGain;
        rPS.FloorCount = FloorCount;
        rPS.RoomCountCleared = RoomCountCleared;
        rPS.EliteRoomCount = EliteRoomCount;
        rPS.RoomCount = RoomCount;
        rPS.ShopCount = ShopCount;
        rPS.RoomLock = RoomLock;
        rPS.GridSize = GridSize;
        rPS.ActiveTimer = ActiveTimer;
        rPS.MalwarePackages = new(MalwarePackages);
        rPS.MineRoomManager = MineRoomManager;
        rPS.FloorManager = FloorManager;
        rPS.FlagMineSelected = FlagMineSelected;
        rPS.Inventory = new(Inventory);
        rPS.BossModification = BossModification;
        rPS.BannedBossModifications = new(BannedBossModifications);
        rPS.mineVisualizer = mineVisualizer;
        SetEffectAbilities(rPS);
    }

    private void SetEffectAbilities(RunPlayerStats rPS)
    {
        rPS.effectAbilities.Clear();
        startEffectAbilities.ForEach(x => rPS.effectAbilities.Add(Instantiate(x)));
        rPS.currentEffectAbility = rPS.effectAbilities.First();
    }

    public void StartObjects(RunPlayerStats rPS)
    {
        GameObject gameObject = Instantiate(MainComponents);
        MainComponents mainComponents = gameObject.GetComponent<MainComponents>();
        if(mainComponents.TryActivateComponents()) rPS.mainComponents = mainComponents;
    }
}
