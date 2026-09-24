using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class SMine : ScriptableObject, ITextable
{
    public int weight = 1;
    public Sprite sprite;
    public int damage = 1;
    public bool isConstant = false;

    public abstract Type GetMineType();

    public virtual void GlobalMineAction()
    {
        if(isConstant)
        {
            //CreateMineType
            GameObject mineInst = new(name);
            mineInst.transform.parent = RunPlayerStats.Instance.mainComponents.GlobalObjectsHolder.transform;
            mineInst.AddComponent(GetMineType());
            Mine tempMine = mineInst.GetComponent<Mine>();
            tempMine.MineData = this;
            tempMine.GlobalMineSubscribe();
            DontDestroyOnLoad(mineInst);
        }
    }

    public virtual void SetUpMine(){}

    public virtual void SendDataToMine(Mine mine){}

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
        List<Vector2> StandardNeighbours = new List<Vector2>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                StandardNeighbours.Add(new Vector2(position.x+x,position.y+y));
            }  
        }
        return StandardNeighbours;
    }


    public abstract string Name { get; }
    public abstract string Description { get; }
    
    public abstract string Rarity { get; }
}
