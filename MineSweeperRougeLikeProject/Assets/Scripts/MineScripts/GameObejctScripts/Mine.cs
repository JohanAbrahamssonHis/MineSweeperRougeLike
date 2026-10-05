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

    public IMineBehavoir currentBehavior;

    protected Mine Context => _context ?? this;

    private Mine _context;

    public void SetContext(Mine mine)
    {
        _context = mine;
    }

    public Mine GlobalMine => _globalMine ?? this;

    private Mine _globalMine;

    public void SetGlobal(Mine mine)
    {
        _globalMine = mine;
    }

    public bool isGlobal;
    public bool hasGlobalChild;

    public virtual void SetUpMine(MineRoomManager mineRoomManager)
    {
        if (MineData == null) return;
        else
        {
            weight = MineData.weight;
            sprite = MineData.sprite;
            damage = MineData.damage;
            MineData.SetUpMine();
        }

        neighbours = new List<Vector2>();
        longnNeighbours = new List<Vector2>();
        this.mineRoomManager = mineRoomManager;
        _spriteRenderer = transform.GetComponent<SpriteRenderer>();
        MineSubscribe();

        SetMineNeighbours();
    }

    public virtual void SendDataConnection() {}

    public virtual void MineSubscribe() {}
    public virtual void MineUnSubscribe() {}

    public virtual void GlobalMineSubscribe() {}
    public virtual void GlobalMineUnSubscribe() {}

    public virtual void MinePicked(){}

    public void OnDisable()
    {
        MineUnSubscribe();
        GlobalMineUnSubscribe();
    }

    public void OnDestroy()
    {
        MineUnSubscribe();
        GlobalMineUnSubscribe();

        if(Context.MineData.isConstant)
        {
            Context.GlobalMine.hasGlobalChild = false;
        }
    }

    public void Update()
    {
        MineUpdate();
        if(Context.MineData.isConstant) GlobalMineUpdate();
    }

    public virtual void MineUpdate() {}

    public virtual void GlobalMineUpdate() {}

    public void SetMineNeighbours()
    {
        if (MineData == null) 
        {
            Debug.LogError(Context.Name + " is not good for neighbour setting");
            return;
        }
        else
        {
            Context.neighbours = MineData.GetNeighbours(position);
            Context.longnNeighbours = MineData.GetLongNeighbours(position);
        }
    }

    public virtual void Activate()
    {
        Context.isActivated = true;
        RunPlayerStats.Instance.Health -= Context.damage;
        SoundManager.Instance.Play("Explosion", transform, true, 1);
    }

    public void SetPosition(Vector2 pos)
    {
        Context.position = pos;
    }

    protected void SetStandardNeighbours(List<Vector2> setNeighbours)
    {
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                setNeighbours.Add(new Vector2(Context.position.x+x,Context.position.y+y));
            }  
        }
    }


    public virtual string Name { get; }
    public virtual string Description { get; }
    public virtual string Rarity { get; }
}
