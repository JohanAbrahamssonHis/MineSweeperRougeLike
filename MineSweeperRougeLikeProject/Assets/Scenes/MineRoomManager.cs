using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineRoomManager : MonoBehaviour
{
    public int mines;

    public Grid grid;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < mines; i++)
        {
            grid.squares[i].hasMine = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
