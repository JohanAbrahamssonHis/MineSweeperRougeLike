using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloorCountVisual : MonoBehaviour
{
    public TMP_Text floorCount;
    
    void Update()
    {
        floorCount.text = RunPlayerStats.Instance.FloorCount.ToString();
    }
}
