using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MainComponents : MonoBehaviour
{
    public GameObject GlobalObjectsHolder;

    List<GameObject> globalGameObjects = new();
    List<Mine> globalMines = new();
    // Start is called before the first frame update
    public bool TryActivateComponents()
    {
        var stats = RunPlayerStats.Instance;

        // Only one instance may exsist
        if (stats.mainComponents != null)
        {
            Destroy(gameObject);

            return false;
        }

        DontDestroyOnLoad(gameObject);  
        return true;  
    }

    public void DestroyGlobalObjects()
    {
        for (int i = GlobalObjectsHolder.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(GlobalObjectsHolder.transform.GetChild(i).gameObject);
        }
    }

    public Mine AddGlobalMine(SMine sMine)
    {
        GameObject mineInst = new(sMine.Name);
        globalGameObjects.Add(mineInst);
        mineInst.transform.parent = GlobalObjectsHolder.transform;
        mineInst.AddComponent(sMine.GetMineType());
        Mine Mine = mineInst.GetComponent<Mine>();
        globalMines.Add(Mine);
        Mine.MineData = sMine;
        Mine.GlobalMinesubscribe();

        return Mine;
    }

    public void DestroyGlobalMine(SMine sMine)
    {

        if(globalMines.Any(x => x.GetType() == sMine.GetMineType()))
        {
            Mine selectedMine = globalMines.First(x => x.GetType() == sMine.GetMineType());
            Destroy(selectedMine.gameObject);
            globalMines.Remove(selectedMine);
            globalGameObjects.Remove(selectedMine.gameObject);
        }
        else Debug.LogError(sMine.name + " was not a global Mine");
    }
}
