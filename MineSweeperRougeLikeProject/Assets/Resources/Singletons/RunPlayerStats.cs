using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "RunPlayerStats", fileName = "RunPlayerStats")]
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

    public int Health { get; set; }
    public float Time { get; set; }
    public int Money { get; set; }
    public int FloorCount { get; set; }
    public int RoomCount { get; set; }
    public List<MalwarePackage> MalwarePackages { get; set; }
    
    public MineRoomManager MineRoomManager { get; set; }
    public FloorManager FloorManager { get; set; }

    public MineViusalizer mineViusalizer { get; set; }
    
    public void ResetValues()
    {
        Health = 4;
        Time = 5 * 60;
        Money = 0;
        FloorCount = 0;
        RoomCount = 0;
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
