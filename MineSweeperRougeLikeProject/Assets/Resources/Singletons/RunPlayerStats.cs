using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Singletons/RunPlayerStats", fileName = "RunPlayerStats")]
public class RunPlayerStats : ScriptableObject
{
    private static RunPlayerStats _instance;
    
    public static RunPlayerStats Instance
    {
        get
        {
            if (_instance == null) _instance = Resources.Load<RunPlayerStats>("Singletons/RunPlayerStats");
            return _instance;
        }
         
    }

    #region Health
    
    //DevLike Immunity
    public bool isInvincable;
    
    //Will trigger events and extra but will remove damage
    public bool isUnDamageable;
    
    public int HealthDamageMultModifier { get; set; }
    public int HealthDamageModifier { get; set; }
    
    private int _health;
    
    public int Health
    {
        get => _health;
        set
        {
            if(isInvincable) return;
            var healthDelta = value < _health ? HealthDamage(value-_health) : HealthGain(value-_health);
            if(isUnDamageable) return;
            _health += healthDelta;
            if(HealthBar!=null) HealthBar.HealthChanged(_health);
            if (_health < 1) Lose();
        }
    }

    private int HealthDamage(int change)
    {
        ActionEvents.Instance.TriggerEventDamage();
        change = (change + HealthDamageModifier) * HealthDamageMultModifier;
        return change;
    }

    private int HealthGain(int change)
    {
        return change;
    }

    public HealthBar HealthBar { get; set; }

    #endregion

    #region Time
    
    public Timmer Timmer { get; set; }
    public bool ActiveTimer { get; set; }
    
    
    private float _time;
    public float Time
    {
        get => _time;
        set
        {
            _time = value;
            if(Timmer==null) return;
            Timmer.SetTimmer();
            if (_time < 0) Lose();
        }
    }

    public float TimeMult { get; set; }
    
    public float TimeGain { get; set; }

    #endregion

    #region Points

    
    public int Points { get; set; }
    public int PointsGain { get; set; }

    #endregion

    #region Heat

    public float Heat { get; set; }
    public float HeatGain { get; set; }
    public float ComboValue { get; set; }
    
    #endregion

    #region Money
    
    private int _money;
    public int Money
    {
        get => _money;
        set
        {
            _money = value;
            SoundManager.Instance.Play("Money", null, true, 1f, 1 + Money * 0.05f);
        }
    }
    public int MoneyGain { get; set; }

    #endregion

    #region Floor
    
    private int _floorCount;
    public int FloorCount
    {
        get => _floorCount;
        set
        {
            _floorCount = value;
            if(value==0) return;
            if (_floorCount % 3 == 2) FloorManager.AddShopRoom(1);
            if (_floorCount % 2 == 0)
            {
                FloorManager.AddEliteRoom(1);
                GridSize += Vector2.one;
            }
            else
            {
                FloorManager.AddBasicRoom(1);
                RoomLock++;
            }
        }
    }

    public int RoomCountCleared { get; set; }
    public int EliteRoomCount { get; set; }
    public int RoomCount { get; set; }
    public int ShopCount { get; set; }
    
    public LockBar LockBar { get; set; }
    private int _roomLock;
    public int RoomLock
    {
        get => _roomLock;
        set
        {
            _roomLock = value;
            if(LockBar!=null) LockBar.FixLocks();
        }
        
    }

    #endregion

    #region Inventory

    
    public List<Item> Inventory { get; set; }
    
    public ItemInventoryVisual ItemInventoryVisual { get; set; }

    public void AddItemToInventory(Item item)
    {
        Inventory.Add(item);
        ItemInventoryVisual.FixVisual();
    }

    #endregion
    
    public Vector2 GridSize { get; set; }
    public MineRoomManager MineRoomManager { get; set; }
    public FloorManager FloorManager { get; set; }
    public MineViusalizer mineVisualizer { get; set; }
    public Mine FlagMineSelected { get; set; }
    public BossModification BossModification { get; set; }
    

    #region End States

    public bool EndState { get; set; }

    public void Lose()
    {
        //ResetValues();
        ActiveTimer = false;
        Inventory.ForEach(x => x.Unsubscribe());
        ResetBoss();
        SceneManager.LoadScene("DeathScene", LoadSceneMode.Additive);
        SoundManager.Instance.Play("GameOver", null, true, 3f);
        SoundManager.Instance.Play("GameOverVoice", null, true, 3f);
    }

    public void Win()
    {
        EndState = true;
        ActiveTimer = false;
        ActionEvents.Instance.TriggerEventMineRoomWin();
        ResetBoss();
    }
    
    #endregion

    #region EndRoomSet

    public float TempTimeGain { get; set; }
    public int TempMoneyGain { get; set; }
    
    public void EndRoomSet()
    {
        TimeEndRoomSet();
        MoneyEndRoomSet();
        
        ResetTempValues();
    }

    public void TimeEndRoomSet()
    {
        Time += TimeGain + TempTimeGain;
    }
    
    public float TimeEndRoomGet()
    {
        return TimeGain + TempTimeGain;
    }
    
    public void MoneyEndRoomSet()
    {
        Money += MoneyGain + (Points / 10) + TempMoneyGain;
    }
    
    public int MoneyEndRoomGet()
    {
        return MoneyGain + Points/10 + TempMoneyGain;
    }

    private void ResetTempValues()
    {
        TempMoneyGain = 0;
        TempTimeGain = 0;
    }

    #endregion

    #region Reset
    
    private void ResetBoss()
    {
        if (FloorManager.currentRoom is not RoomBossMine) return;
        BossModification?.UnsubscribeModification();
    }
    
    public void ResetValues()
    {
        HealthDamageModifier = 0;
        HealthDamageMultModifier = 1;
        Health = 5;
        Time = 3*60;
        TimeMult = 1;
        TimeGain = 15;
        Money = 5;
        MoneyGain = 2;
        Points = 0;
        PointsGain = 1;
        Heat = 0;
        HeatGain = 0.15f;
        FloorCount = 0;
        RoomCountCleared = 0;
        EliteRoomCount = 1;
        RoomCount = 2;
        ShopCount = 2;
        RoomLock = 2;
        GridSize = new Vector2(6, 6);
        ActiveTimer = false;
        MalwarePackages = new List<MalwarePackage>();
        MineRoomManager = null;
        FloorManager = null;
        FlagMineSelected = null;
        Inventory = new List<Item>();
    }

    #endregion

    #region MalwarePackage
    public List<MalwarePackage> MalwarePackages { get; set; }

    public void AddMalwarePackage(MalwarePackage malwarePackage)
    {
        MalwarePackages.Add(Instantiate(malwarePackage));
        if(mineVisualizer==null) return;
        mineVisualizer.SetVisualizer();
    }

    public void SetMineVisualizer()
    {
        if(mineVisualizer==null) return;
        mineVisualizer.SetVisualizer();
    }

    #endregion

}
