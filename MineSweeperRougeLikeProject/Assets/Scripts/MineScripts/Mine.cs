using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Mine : MonoBehaviour
{
    public int weight;
    public Sprite sprite;
    public Vector2 position;
    public List<Vector2> neighbours;
    public delegate void CallBack(string hello);
    public SpriteRenderer _spriteRenderer;
    public MineRoomManager mineRoomManager;

    public CallBack call;
    
    // Start is called before the first frame update
    public virtual void SetUpMine(MineRoomManager mineRoomManager)
    {
        /*
        if (GetComponent<SpriteRenderer>() == null) gameObject.AddComponent<SpriteRenderer>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = null;
        */
        neighbours = new List<Vector2>();
        this.mineRoomManager = mineRoomManager;
    }

    public void SetPosition(Vector2 pos)
    {
        position = pos;
    }
    
    public void GetSpriteOrder(int order)
    {
        Debug.Log($"Hello {order}");
        _spriteRenderer.sortingOrder = order;
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
}
