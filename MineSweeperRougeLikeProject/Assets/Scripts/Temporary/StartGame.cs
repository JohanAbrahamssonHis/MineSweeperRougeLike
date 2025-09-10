using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour, IInteractable
{
    public MalwarePackage StartPackage;
    public void Interact()
    {
        RunPlayerStats.Instance.ResetValues();
        RunPlayerStats.Instance.AddMalwarePackage(StartPackage);
        SceneManager.LoadScene("FloorScene");
    }
}
