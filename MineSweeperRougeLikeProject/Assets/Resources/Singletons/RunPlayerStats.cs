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

}
