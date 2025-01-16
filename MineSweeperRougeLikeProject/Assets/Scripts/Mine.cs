using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Mine : MonoBehaviour
{
    public int weight;
    public Sprite sprite;
    public Vector2 position;
    public delegate void CallBack(string hello);

    public CallBack call;
    
    // Start is called before the first frame update
    public void SetUpMine()
    {
        SpriteRenderer spriteRenderer = this.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }

    public void SetPostion(Vector2 pos)
    {
        position = pos;
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
