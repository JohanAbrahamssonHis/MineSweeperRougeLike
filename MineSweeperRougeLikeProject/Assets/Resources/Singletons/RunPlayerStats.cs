using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
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

    private int health;
    public bool isInvincable;
    public int Health
    {
        get => health;
        set
        {
            if(isInvincable) return;
            health = value;
            if (health < 0)
            {
                Lose();
            }
        }
    }


    public Timmer Timmer;
    public bool ActiveTimer;
    private float time;
    public float Time
    {
        get => time;
        set
        {
            time = value;
            Timmer.SetTimmer();
            if (time < 0)
            {
                Lose();
            }
        }
    }
    public int Money { get; set; }
    private int floorCount;
    public int FloorCount
    {
        get => floorCount;
        set
        {
            floorCount = value;
            if (floorCount % 2 == 1) RoomLock++;
            else GridSize += Vector2.one;
        }
    }
    public int RoomCount { get; set; }
    public int RoomLock { get; set; }
    
    public Vector2 GridSize { get; set; }
    public List<MalwarePackage> MalwarePackages { get; set; }
    
    public MineRoomManager MineRoomManager { get; set; }
    public FloorManager FloorManager { get; set; }

    public MineViusalizer mineViusalizer { get; set; }

    public void Lose()
    {
        ResetValues();
        SceneDeterminer.ReturnToFloorFromLose();
    }
    
    public void ResetValues()
    {
        Health = 4;
        Time = 4*60;
        Money = 10;
        FloorCount = 0;
        RoomCount = 0;
        RoomLock = 2;
        GridSize = new Vector2(6, 6);
        MalwarePackages = new List<MalwarePackage>();
        MineRoomManager = null;
        FloorManager = null;
    }

    public void AddMalwarePackage(MalwarePackage malwarePackage)
    {
        MalwarePackages.Add(malwarePackage);
        mineViusalizer.SetVisualizer();
    }

}
