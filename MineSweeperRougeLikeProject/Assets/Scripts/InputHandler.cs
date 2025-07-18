using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputHandler : MonoBehaviour
{
    private Camera _mainCamera;
    
    void Awake()
    {
        _mainCamera = Camera.main;
    }
    
    
    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if(!rayHit.collider) return;

        if (!rayHit.collider.gameObject.TryGetComponent(out IInteractable interactable)) return;
        interactable.Interact();
    }
    
    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if(!rayHit.collider) return;

        if (!rayHit.collider.gameObject.TryGetComponent(out IInteractable interactable)) return;
        interactable.SecondInteract();
    }
    
    public void OnResetBoard(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        RunPlayerStats.Instance.ResetValues(); 
        /*
        RunPlayerStats.Instance?.MineRoomManager.ResetBoard();
        RunPlayerStats.Instance?.FloorManager.ResetBoard();
        */
    }
}
