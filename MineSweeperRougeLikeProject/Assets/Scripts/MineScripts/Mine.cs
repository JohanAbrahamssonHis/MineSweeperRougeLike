using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Mine : MonoBehaviour, ITextable
{
    public bool isDisabled;
    public int weight;
    public Sprite sprite;
    public Vector2 position;
    public List<Vector2> neighbours;
    public List<Vector2> longnNeighbours;
    public bool isActivated;
    public delegate void CallBack(string hello);
    public SpriteRenderer _spriteRenderer;
    public MineRoomManager mineRoomManager;
    public int damage = 1;

    public CallBack call;

    public virtual void SetUpMine(MineRoomManager mineRoomManager)
    {
        neighbours = new List<Vector2>();
        this.mineRoomManager = mineRoomManager;
    }

    public virtual void Activate()
    {
        isActivated = true;
        RunPlayerStats.Instance.Health -= damage;
        SoundManager.Instance.Play("Explosion", transform, true, 1);
    }

    public void SetPosition(Vector2 pos)
    {
        position = pos;
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

    public abstract string Name { get; }
    public abstract string Description { get; }
    
    public abstract string Rarity { get; }
}
