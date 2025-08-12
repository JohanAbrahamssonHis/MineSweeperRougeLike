using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Singletons/ActionEvents", fileName = "ActionEvents")]
public class ActionEvents : ScriptableObject
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
    public event ActionEvent OnDamage;

    public void TriggerEventAction() => OnAction?.Invoke();
    public void TriggerEventFlag() => OnFlag?.Invoke();
    public void TriggerEventDamage() => OnDamage?.Invoke();
}
