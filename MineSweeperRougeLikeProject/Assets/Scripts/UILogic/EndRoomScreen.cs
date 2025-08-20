using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class EndRoomScreen : MonoBehaviour
{
    public TMP_Text pointsAmount;
    public TMP_Text moneyAmount;
    public TMP_Text timeAmount;
    //TODO: fix this
    //private bool tempLock;


    public void SetScreen(bool state)
    {
        //if(tempLock) return;
        //tempLock = true;
        
        gameObject.SetActive(state);
        
        pointsAmount.text = RunPlayerStats.Instance.Points.ToString();

        //TODO: Put this to a better place
        RunPlayerStats.Instance.Money += RunPlayerStats.Instance.Points / 10;
        
        moneyAmount.text = $"+{RunPlayerStats.Instance.Points / 10}";

        //TODO: make this a variable
        timeAmount.text = "+30";
    }

    public void ReturnToFloor()
    {
        SceneDeterminer.ReturnToFloor(RunPlayerStats.Instance.FloorManager.currentRoom.scene);
    }

}
