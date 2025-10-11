using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Room : MonoBehaviour
{
    public Sprite sprite;
    public Vector2 position;
    public List<Vector2> neighbours;
    public FloorManager floorManager;
    public string scene;
    public string _garageDoorSceneName = "GarageDoorScene";
    
    public void SetPosition(Vector2 pos)
    {
        position = pos;
    }
    
    public virtual void SetUpRoom(FloorManager floorManager)
    {
        neighbours = new List<Vector2>();
        this.floorManager = floorManager;
    }
    
    protected void SetStandardNeighbours(List<Vector2> setNeighbours)
    {
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                setNeighbours.Add(new Vector2(position.x+x,position.y+y));
            }  
        }
    }

    public virtual void RoomFunction()
    {
        //What the room does when Clicked on
    }

    public virtual void LeaveRoomFunction()
    {
        //LeavingRoomFunction
    }
}
