using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour, IInteractable, ITextable
{
    public List<MalwarePackage> StartPackages;

    public string Name => "Start The Game";

    public string Description => "";

    public void Interact()
    {
        RunPlayerStats.Instance.ResetValues();
        //StartPackages.ForEach(x => RunPlayerStats.Instance.AddMalwarePackage(x));
        SceneManager.LoadScene("FloorScene");
    }

    public void OnEnable()
    {
        StartData.Instance.StartObjects(RunPlayerStats.Instance);
    }
}
