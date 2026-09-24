using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Mine : MonoBehaviour, ITextable
{
    public SMine MineData;

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

    public virtual void SetUpMine(MineRoomManager mineRoomManager)
    {
        if (MineData == null) return;
        else
        {
            weight = MineData.weight;
            sprite = MineData.sprite;
            damage = MineData.damage;
            MineData.SetUpMine();
            MineData.SendDataToMine(this);
        }

        neighbours = new List<Vector2>();
        longnNeighbours = new List<Vector2>();
        this.mineRoomManager = mineRoomManager;
        _spriteRenderer = transform.GetComponent<SpriteRenderer>();
        MineSubscribe();

        SetMineNeighbours();
    }

    public virtual void MineSubscribe() {}
    public virtual void MineUnSubscribe() {}

    public virtual void GlobalMineSubscribe() {}
    public virtual void GlobalMineUnSubscribe() {}

    public void OnDisable()
    {
        MineUnSubscribe();
        GlobalMineUnSubscribe();
    }

    public void OnDestroy()
    {
        MineUnSubscribe();
        GlobalMineUnSubscribe();
    }

    public void SetMineNeighbours()
    {
        if (MineData == null) return;
        else
        {
            neighbours = MineData.GetNeighbours(position);
            longnNeighbours = MineData.GetLongNeighbours(position);
        }
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


    public virtual string Name { get; }
    public virtual string Description { get; }
    public virtual string Rarity { get; }
}
