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


    public void SetScreen(bool state)
    {
        gameObject.SetActive(state);
        
        pointsAmount.text = RunPlayerStats.Instance.Points.ToString();
        
        moneyAmount.text = $"+{RunPlayerStats.Instance.MoneyGain + RunPlayerStats.Instance.Points/10}";
        
        timeAmount.text = $"+{RunPlayerStats.Instance.TimeGain}";
    }
}
