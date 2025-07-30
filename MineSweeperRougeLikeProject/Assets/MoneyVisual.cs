using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyVisual : MonoBehaviour
{
    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = $"{RunPlayerStats.Instance.Money.ToString()}$";
    }
}
