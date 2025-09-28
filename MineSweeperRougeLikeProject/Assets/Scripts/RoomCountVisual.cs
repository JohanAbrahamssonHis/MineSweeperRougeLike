using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomCountVisual : MonoBehaviour
{
    public TMP_Text RoomTextCount;
    public TMP_Text ShopTextCount;
    public TMP_Text EliteTextCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RoomTextCount.text = RunPlayerStats.Instance.RoomCount.ToString();
        ShopTextCount.text = RunPlayerStats.Instance.ShopCount.ToString();
        EliteTextCount.text = RunPlayerStats.Instance.EliteRoomCount.ToString();
    }
}
