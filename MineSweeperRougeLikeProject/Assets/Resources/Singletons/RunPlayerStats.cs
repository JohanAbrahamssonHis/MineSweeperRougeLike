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
    
    
    public bool isInvincable;
    private int health;
    public int Health
    {
        get => health;
        set
        {
            if(isInvincable) return;
            if (value < health) ActionEvents.Instance.TriggerEventDamage();
            health = value;
            if(HealthBar!=null) HealthBar.HealthChanged(value);
            if (health < 1) Lose();
        }
    }

    public HealthBar HealthBar { get; set; }


    public Timmer Timmer;
    public bool ActiveTimer;
    private float time;
    public float Time
    {
        get => time;
        set
        {
            time = value;
            if(Timmer==null) return;
            Timmer.SetTimmer();
            if (time < 0)
            {
                Lose();
            }
        }
    }
    
    public float TimeGain { get; set; }
    
    public int Points { get; set; }
    public float Heat { get; set; }
    public float ComboValue { get; set; }
    public int Money { get; set; }
    public int MoneyGain { get; set; }
    
    private int floorCount;
    public int FloorCount
    {
        get => floorCount;
        set
        {
            floorCount = value;
            if(value==0) return;
            FloorManager.AddBasicRoom(1);
            if (floorCount % 3 == 2) FloorManager.AddShopRoom(1);
            if (floorCount % 2 == 1) RoomLock++;
            else GridSize += Vector2.one;
        }
    }

    public List<Item> Inventory;
    
    public ItemInventoryVisual ItemInventoryVisual;

    public void AddItemToInventory(Item item)
    {
        Inventory.Add(item);
        ItemInventoryVisual.FixVisual();
    }
    
    
    public int RoomCount { get; set; }
    
    public LockBar lockBar { get; set; }
    private int roomLock;
    public int RoomLock
    {
        get => roomLock;
        set
        {
            roomLock = value;
            if(lockBar!=null) lockBar.FixLocks();
        }
        
    }
    
    public Vector2 GridSize { get; set; }
    public List<MalwarePackage> MalwarePackages { get; set; }
    
    public MineRoomManager MineRoomManager { get; set; }
    public FloorManager FloorManager { get; set; }

    public MineViusalizer mineViusalizer { get; set; }
    public Mine FlagMineSelected { get; set; }

    public void Lose()
    {
        //ResetValues();
        Inventory.ForEach(x => x.Unsubscribe());
        SceneManager.LoadScene("DeathScene", LoadSceneMode.Additive);
    }

    public bool endState { get; set; }
    public void Win()
    {
        endState = true;
        ActiveTimer = false;
        ActionEvents.Instance.TriggerEventMineRoomWin();
    }
    
    public void ResetValues()
    {
        Health = 5;
        Time = 3*60;
        TimeGain = 30;
        Money = 5;
        MoneyGain = 4;
        FloorCount = 0;
        RoomCount = 0;
        RoomLock = 2;
        GridSize = new Vector2(6, 6);
        MalwarePackages = new List<MalwarePackage>();
        MineRoomManager = null;
        FloorManager = null;
        FlagMineSelected = null;
        Inventory = new List<Item>();
    }

    public void AddMalwarePackage(MalwarePackage malwarePackage)
    {
        MalwarePackages.Add(Instantiate(malwarePackage));
        if(mineViusalizer==null) return;
        mineViusalizer.SetVisualizer();
    }

    public void SetMineVisualizer()
    {
        if(mineViusalizer==null) return;
        mineViusalizer.SetVisualizer();
    }

}
