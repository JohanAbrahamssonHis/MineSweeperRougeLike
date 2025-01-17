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
    SpriteRenderer _spriteRenderer;

    public CallBack call;
    
    // Start is called before the first frame update
    public void SetUpMine()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = sprite;
    }

    public void SetPosition(Vector2 pos)
    {
        position = pos;
    }
    
    public void GetSpriteOrder(int order)
    {
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
}
