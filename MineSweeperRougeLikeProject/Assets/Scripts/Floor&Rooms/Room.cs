using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Sprite sprite;
    public Vector2 position;
    public List<Vector2> neighbours;
    
    public void SetPosition(Vector2 pos)
    {
        position = pos;
    }
}
