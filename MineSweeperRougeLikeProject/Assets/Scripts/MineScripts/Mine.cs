using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mine
{
    public int weight;
    public Sprite sprite;
    public Vector2 position;
    public List<Vector2> neighbours;
    public delegate void CallBack(string hello);
    public SpriteRenderer _spriteRenderer;

    public CallBack call;

    public Mine Clone<T>(T mine) where T : Mine 
    {
        //return new T();
        return null;
    }
    
    // Start is called before the first frame update
    public virtual void SetUpMine()
    {
        /*
        if (GetComponent<SpriteRenderer>() == null) gameObject.AddComponent<SpriteRenderer>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = null;
        */
        neighbours = new List<Vector2>();
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

    public void Suckda(string hello)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        call += Suckda;
        call += Suckda;

        
        
        call("hello");

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
