using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputHandler : MonoBehaviour
{
    private Camera _mainCamera;
    
    void Start()
    {
        _mainCamera = RunPlayerStats.Instance.Camera;
    }
    
    
    public void OnClick(InputAction.CallbackContext context)
    {
        ButtonEffect(context)?.ForEach(x => x.Interact());
    }
    
    public void OnRightClick(InputAction.CallbackContext context)
    {
        ButtonEffect(context)?.ForEach(x => x.SecondInteract());
    }
    
    public void OnResetBoard(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        //RunPlayerStats.Instance.ResetValues(); 
        /*
        RunPlayerStats.Instance?.MineRoomManager.ResetBoard();
        RunPlayerStats.Instance?.FloorManager.ResetBoard();
        */
    }

    public void OnScroll(InputAction.CallbackContext context)
    {
        ButtonEffect(context)?.ForEach(x => x.Scroll(context.ReadValue<float>()));
    }
    
    public void OnWheelButton(InputAction.CallbackContext context)
    {
        ButtonEffect(context)?.ForEach(x => x.WheelButton());
    }

    
    private List<IInteractable> ButtonEffect(InputAction.CallbackContext context)
    {
        if (!context.performed) return null;
        List<IInteractable> interactables = new List<IInteractable>();
        if (_mainCamera is null) Debug.LogError("fiehfei");
        var rayHits = Physics2D.GetRayIntersectionAll(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if(rayHits.Any(x => !x.collider)) return null;

        foreach (var rayHit in rayHits)
        {
            if (!rayHit.collider.gameObject.TryGetComponent(out IInteractable interactable)) continue;
            interactables.Add(interactable);
        }

        return interactables;
    }
    
    /*
    private IInteractable ButtonEffect(InputAction.CallbackContext context)
    {
        if (!context.performed) return null;
        Debug.Log("hello");
        //List<IInteractable> interactables = new List<IInteractable>();
        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if(!rayHit.collider) return null;

        
        if (!rayHit.collider.gameObject.TryGetComponent(out IInteractable interactable)) return null;

        return interactable;
    }
    */
}
