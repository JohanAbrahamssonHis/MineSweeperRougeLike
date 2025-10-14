using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Singletons/SceneDeterminer", fileName = nameof(SceneDeterminer))]
public class SceneDeterminer : ScriptableObject
{
    private static SceneDeterminer _instance;

    public static SceneDeterminer Instance
    {
        get
        {
            if (_instance == null) _instance = Resources.Load<SceneDeterminer>("Singletons/SceneDeterminer");
            return _instance;
        }
     
    }

    public static FloorManager FloorManager { get; set; }
    public RoomStartVisual RoomStartVisual { get; set; }

    public void LoadAddedSceneGarage()
    {
        SceneManager.LoadScene("GarageDoorScene", LoadSceneMode.Additive);
        //Fix for null reference
        //RoomStartVisual = Object.FindObjectOfType<RoomStartVisual>();
    }


    public static void LoadAddedScene(string sceneName)
    {
        RunPlayerStats.Instance.FloorManager.DisableFloor(false);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
    
    public static void ReturnToFloor(string sceneName)
    {
        if (RunPlayerStats.Instance.FloorManager.currentRoom is not RoomBossMine) Instance.RoomStartVisual?.CloseDoor(sceneName);
        else ReturnToFloorAfter(sceneName);
    }

    public static void ReturnToFloorAfter(string sceneName)
    {
        FloorManager floorManager = RunPlayerStats.Instance.FloorManager;

        floorManager.DisableFloor(true);
        RoomBossMine rb = floorManager.bossRoom.room as RoomBossMine;
        if (RunPlayerStats.Instance.FloorManager.currentRoom is not RoomBossMine) floorManager.currentRoom?.LeaveRoomFunction();
        rb.CheckActivation();
        SceneManager.UnloadSceneAsync(sceneName);
        SceneManager.UnloadSceneAsync("GarageDoorScene");
        floorManager.DoorAnimationClose();
    }

    public static void ReturnToFloorBoss(string sceneName)
    {
        FloorManager floorManager = RunPlayerStats.Instance.FloorManager;
        Instance.RoomStartVisual?.CloseDoor(sceneName);
        SceneManager.UnloadSceneAsync(sceneName);
        floorManager.currentRoom?.LeaveRoomFunction();
    }
    
    public static void ReturnToFloorFromLose()
    {
        SceneManager.LoadScene("FloorScene");
    }
}
