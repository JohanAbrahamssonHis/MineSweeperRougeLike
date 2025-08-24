using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        SceneManager.LoadScene("StartScene");
    }
}