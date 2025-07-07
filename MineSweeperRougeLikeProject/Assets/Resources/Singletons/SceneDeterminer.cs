using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Scene Determiner", fileName = nameof(SceneDeterminer))]
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


    public static void LoadAddedScene(string sceneName)
    {
        FloorManager.DisableFloor(false);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
    
    public static void ReturnToFloor(string sceneName)
    {
        FloorManager.DisableFloor(true);
        FloorManager.currentRoom.LeaveRoomFunction();
        RoomBossMine rb = FloorManager.bossRoom.room as RoomBossMine;
        rb.CheckActivation();
        SceneManager.UnloadSceneAsync(sceneName);
    }
}
