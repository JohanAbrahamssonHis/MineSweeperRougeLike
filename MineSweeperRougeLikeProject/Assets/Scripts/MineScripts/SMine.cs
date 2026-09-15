using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class SMine : ScriptableObject, ITextable
{

    public bool isDisabled;
    public int weight;
    public Sprite sprite;
    public Vector2 position;
    public List<Vector2> neighbours;
    public List<Vector2> longnNeighbours;
    public bool isActivated;
    public SpriteRenderer _spriteRenderer;
    public MineRoomManager mineRoomManager;
    public int damage = 1;
    private Transform _transform;
    public Transform transform
    {
        set => _transform = value; get => _transform;
    }

    /*
    public virtual void SetUpMine(MineRoomManager mineRoomManager)
    {
        neighbours = new List<Vector2>();
        longnNeighbours = new List<Vector2>();
        this.mineRoomManager = mineRoomManager;
    }
    */
    /*
    public virtual void Activate()
    {
        isActivated = true;
        RunPlayerStats.Instance.Health -= damage;
        SoundManager.Instance.Play("Explosion", _transform, true, 1);
    }
    */
    /*
    public void SetPosition(Vector2 pos)
    {
        position = pos;
    }
    */

    public virtual List<Vector2> GetNeighbours(Vector2 pos)
    {
        return GetStandardNeighbours(pos);
    }

    public virtual List<Vector2> GetLongNeighbours(Vector2 pos)
    {
        return new List<Vector2>();
    }

    protected List<Vector2> GetStandardNeighbours(Vector2 pos)
    {
        Vector2 position = pos;
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                neighbours.Add(new Vector2(position.x+x,position.y+y));
            }  
        }
        return neighbours;
    }


    public abstract string Name { get; }
    public abstract string Description { get; }
    
    public abstract string Rarity { get; }
}
