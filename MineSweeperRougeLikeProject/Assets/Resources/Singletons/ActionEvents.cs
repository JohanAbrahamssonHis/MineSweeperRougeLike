using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    
    public delegate void ActionEventShop(ShopManager shopManager);
    
    
    //First Action Of the game
    public event ActionEvent OnFirstAction;
    public void TriggerEventFirstAction() => OnFirstAction?.Invoke();
    
    //When a box is clicked (aka an action)
    public event ActionEvent OnAction;
    public void TriggerEventAction() => OnAction?.Invoke();
    
    //After an action has been made
    public event ActionEvent OnAfterAction;
    public void TriggerEventAfterAction() => OnAfterAction?.Invoke();
    
    //When you win a room
    public event ActionEvent OnMineRoomWin;
    public void TriggerEventMineRoomWin() => OnMineRoomWin?.Invoke();
    
    //When Flag is placed
    public event ActionEvent OnFlag;
    public void TriggerEventFlag() => OnFlag?.Invoke();
    
    //When You take Damage
    public event ActionEvent OnDamage;
    public void TriggerEventDamage() => OnDamage?.Invoke();
    
    //When shop is entered
    public event ActionEventShop OnShop;
    public void TriggerEventShop(ShopManager shopManager) => OnShop?.Invoke(shopManager);
    
    //After shop is entered
    public event ActionEventShop OnShopAfter;
    public void TriggerEventShopAfter(ShopManager shopManager) => OnShopAfter?.Invoke(shopManager);
    
    //Leaving Shop
    public event ActionEventShop OnShopLeave;
    public void TriggerEventShopLeave(ShopManager shopManager) => OnShopLeave?.Invoke(shopManager);

}
