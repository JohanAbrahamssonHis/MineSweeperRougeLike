using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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


    public void LoadAddedScene(string sceneName)
    {
        FloorManager.DisableFloor(false);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
    
    public void ReturnToFloor(string sceneName)
    {
        Debug.Log("zello");
        FloorManager.DisableFloor(true);
        Debug.Log("rello");
        SceneManager.UnloadSceneAsync(sceneName);
    }
}
