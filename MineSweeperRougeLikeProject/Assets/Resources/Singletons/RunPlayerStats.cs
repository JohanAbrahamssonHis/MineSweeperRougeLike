using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

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

    private StartData startData;
    public MainComponents mainComponents;
    public bool removeTimeValues;

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
            float temp = _time;
            _time = value;
            if(Timmer==null) return;
            Timmer.SetTimmer();
            if(temp<value) Timmer.FixBeepTimmer();
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
            if(setUpState) return;
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
            //FloorCount is used to determine what rooms are added to the floor. It is not the actual floor count, but rather a counter for how many floors have been completed.
            _floorCount = value;

            // If the value is 1, we are already on the first floor, so we return early.
            if(value==1) return;

            // If the value is greater than 1, we are moving to a new floor, so we add rooms based on the floor count. 
            if (_floorCount % 3 == 0) ShopCount++;
            if (_floorCount % 2 == 1)
            {
                EliteRoomCount++;
                GridSize += Vector2.one;
            }
            else
            {
                RoomCount++;
                RoomLock++;
            }

            // Add stats for the new floor. Each new floor gives the player 1 health and 60 seconds of time.
            Health += 1;
            Time += 60;
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
    public ShopManager ShopManager { get; set; }
    public MineViusalizer mineVisualizer { get; set; }
    public SMine FlagMineSelected { get; set; }
    public BossModification BossModification { get; set; }

    public bool DebugMode;
    public bool setUpState;
    
    [System.NonSerialized] private Camera _camera;
    public Camera Camera {
        get => _camera;
        set => _camera = value;
    }
    // Säker reset vid uppstart (oavsett domain reload-inställning)
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void ResetRuntimeState()
    {
        if (Instance != null) Instance._camera = null;
    }

    public List<EffectAbility> effectAbilities;
    public EffectAbility currentEffectAbility;

    public EffectAbility GetNextEffectAbility()
    {
        return effectAbilities.Last() == currentEffectAbility ? effectAbilities.First() : effectAbilities[effectAbilities.IndexOf(currentEffectAbility) + 1];
    }
    
    public List<string> BannedBossModifications { get; set; }

    public void SetBossModification()
    {
        if (BannedBossModifications.Count == BossModificationLibrary.Instance.BossModifications.Count)
            ResetBannedBosses();
        

        if(BossModificationLibrary.Instance.UseSetBossModification)
        {
            BossModification = Instantiate(BossModificationLibrary.Instance.SetBossModification);
            if(FloorManager != null)
                FloorManager.bossRoom.SetBossModificationSprite(BossModification.sprite);
            return;
        }


        List<BossModification> bossModifications = BossModificationLibrary.Instance.BossModifications.Where(x =>
            !BannedBossModifications.Contains(x.name)).ToList();
        
        BossModification = Instantiate(bossModifications[Random.Range(0,bossModifications.Count)]);
        if(FloorManager != null)
            FloorManager.bossRoom.SetBossModificationSprite(BossModification.sprite);
        BannedBossModifications.Add(BossModification.name);
    }

    private TextVisualObject _textVisualObject;
    public TextVisualObject TextVisualObject
    {
        get => _textVisualObject;
        set
        {
            if(_textVisualObject!=null && value != null) return;
            _textVisualObject = value;
        } 
    }
    

    #region End States

    public bool EndState { get; set; }

    public void Lose()
    {
        //ResetValues();
        ActiveTimer = false;
        Inventory.ForEach(x => x.Unsubscribe());
        effectAbilities.ForEach(x => x.ResetAbility());
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
        effectAbilities.ForEach(x => x.ResetAbility());
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
        Money += MoneyEndRoomGet();
    }
    
    public int MoneyEndRoomGet()
    {
        return MoneyGain + Points/50 + TempMoneyGain;
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
        setUpState = true;
        startData = StartData.Instance;
        startData.StartValues(this);
        setUpState = false;
    }

    private void ResetBannedBosses()
    {
        BannedBossModifications.Clear();
    }

    #endregion

    #region MalwarePackage
    public List<MalwarePackage> MalwarePackages { get; set; }

    public void AddMalwarePackage(MalwarePackage malwarePackage)
    {
        MalwarePackages.Add(Instantiate(malwarePackage));
        if(mineVisualizer is null) return;
        mineVisualizer.SetVisualizer();
    }

    public void SetMineVisualizer()
    {
        if(mineVisualizer is null) return;
        mineVisualizer.SetVisualizer();
    }

    #endregion

}
