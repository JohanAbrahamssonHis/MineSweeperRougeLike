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

    private string valueSymbol;


    public void SetScreen(bool state)
    {
        gameObject.SetActive(state);
        
        pointsAmount.text = RunPlayerStats.Instance.Points.ToString();

        valueSymbol = RunPlayerStats.Instance.MoneyEndRoomGet() >= 0 ? "+" : "-";  
        moneyAmount.text = $"{valueSymbol}{RunPlayerStats.Instance.MoneyEndRoomGet()}";
        
        valueSymbol = RunPlayerStats.Instance.TimeEndRoomGet() >= 0 ? "+" : "-"; 
        timeAmount.text = $"{valueSymbol}{RunPlayerStats.Instance.TimeEndRoomGet()}";
    }
}
