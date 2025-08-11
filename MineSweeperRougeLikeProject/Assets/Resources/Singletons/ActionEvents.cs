using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "RunPlayerStats", fileName = "RunPlayerStats")]
public class ActionEvents : MonoBehaviour
{
    private static ActionEvents _instance;
    
    public static ActionEvents Instance
    {
        get
        {
            if (_instance == null) _instance = Resources.Load<ActionEvents>("Singletons/ActionEvents");
            return _instance;
        }
    }

    public delegate void ActionEvent();
    
    public event ActionEvent OnAction;
    public event ActionEvent OnFlag;
    
}
