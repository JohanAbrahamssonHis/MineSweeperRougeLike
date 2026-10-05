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

    public delegate void ActionEventPosition(Vector2 position);

    public delegate void ActionEventSMine(SMine SMine);

    //On Start of a run
    public event ActionEvent OnStartRun;
    public void TriggerEventStartRun() => OnStartRun?.Invoke();
    
    //First Action Of the game
    public event ActionEvent OnFirstAction;
    public void TriggerEventFirstAction() => OnFirstAction?.Invoke();
    
    //When a box is clicked (aka an action)
    public event ActionEvent OnAction;
    public void TriggerEventAction() => OnAction?.Invoke();
    
    //After an action has been made
    public event ActionEvent OnAfterAction;
    public void TriggerEventAfterAction() => OnAfterAction?.Invoke();

    //After first action has been made
    public event ActionEvent OnAfterFirstAction;
    public void TriggerEventAfterFirstAction() => OnAfterFirstAction?.Invoke();

    //After the basic rest has been made
    public event ActionEvent OnAfterReset;
    public void TriggerEventAfterReset() => OnAfterReset?.Invoke();
    
    //When you win a room
    public event ActionEvent OnMineRoomWin;
    public void TriggerEventMineRoomWin() => OnMineRoomWin?.Invoke();
    
    //When Flag is placed
    public event ActionEvent OnFlag;
    public void TriggerEventFlag() => OnFlag?.Invoke();
    
    //When You take Damage
    public event ActionEvent OnDamage;
    public void TriggerEventDamage() => OnDamage?.Invoke();

    //When You gain Health
    public event ActionEvent OnHealthGain;
    public void TriggerEventHealthGain() => OnHealthGain?.Invoke();

    //When 1 second of Timer time has passed
    public event ActionEvent On1SecondTimerPassed;
    public void TriggerEvent1SecondTimerPassed() => On1SecondTimerPassed?.Invoke();

    //When the timer is activated
    public event ActionEvent OnTimerActivated;
    public void TriggerEventTimerActivated() => OnTimerActivated?.Invoke();

    //When beginLogic function has started
    public event ActionEvent OnBeginLogic;
    public void TriggerEventBeginLogic() => OnBeginLogic?.Invoke();
    
    //When the player leaves a non shop room
    public event ActionEvent OnLeaveRoom;
    public void TriggerEventLeaveRoom() => OnLeaveRoom?.Invoke();
    
    //When shop is entered
    public event ActionEventShop OnShop;
    public void TriggerEventShop(ShopManager shopManager) => OnShop?.Invoke(shopManager);
    
    //After shop is entered
    public event ActionEventShop OnShopAfter;
    public void TriggerEventShopAfter(ShopManager shopManager) => OnShopAfter?.Invoke(shopManager);

    //After Item is bought
    public event ActionEvent OnShopBought;
    public void TriggerEventShopBought() => OnShopBought?.Invoke();

    
    //Leaving Shop
    public event ActionEventShop OnShopLeave;
    public void TriggerEventShopLeave(ShopManager shopManager) => OnShopLeave?.Invoke(shopManager);

    //When a Square is activated
    public event ActionEventPosition OnSquareActivate;
    public void TriggerEventSquareActivate(Vector2 Position) => OnSquareActivate?.Invoke(Position);
    
    //When a Square is activated
    public event ActionEventSMine OnMinePicked;
    public void TriggerEventMinePicked(SMine SMine) => OnMinePicked?.Invoke(SMine);
}
