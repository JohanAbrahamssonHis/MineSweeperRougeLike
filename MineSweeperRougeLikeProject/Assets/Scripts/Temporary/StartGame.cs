using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour, IInteractable
{
    public List<MalwarePackage> StartPackages;
    public void Interact()
    {
        RunPlayerStats.Instance.ResetValues();
        StartPackages.ForEach(x => RunPlayerStats.Instance.AddMalwarePackage(x));
        SceneManager.LoadScene("FloorScene");
    }
}
