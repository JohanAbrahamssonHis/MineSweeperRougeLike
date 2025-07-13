using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MineViusalizer : MonoBehaviour
{
    public GameObject visualObject;
    public MalwarePackage malwarePackage;

    public bool startValue;
    void Start()
    {
        RunPlayerStats.Instance.MalwarePackages.Add(malwarePackage);
    }

    
    void Update()
    {
        if (!startValue)
        {
            RunPlayerStats.Instance.MalwarePackages.Add(malwarePackage);
            startValue = true;
        }

        Debug.Log(RunPlayerStats.Instance.MalwarePackages.Count);
        List<Mine> mines = new List<Mine>();

        RunPlayerStats.Instance.MalwarePackages.ForEach(x => x.mines.ForEach( mine =>mines.Add(mine)));

        var orderedEnumerable = mines.OrderBy(mine => mine.name).ToList();

        mines = orderedEnumerable;
        
        foreach (var t in mines)
        {
            Debug.Log(t);
        }
    }
}
