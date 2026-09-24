using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainComponents : MonoBehaviour
{
    public GameObject GlobalObjectsHolder;
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
}
