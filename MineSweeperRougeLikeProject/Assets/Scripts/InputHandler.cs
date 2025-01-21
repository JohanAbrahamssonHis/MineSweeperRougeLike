using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputHandler : MonoBehaviour
{
    private Camera _mainCamera;
    public MineRoomManager _mineRoomManager;
    
    void Awake()
    {
        _mainCamera = Camera.main;
    }
    
    
    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if(!rayHit.collider) return;

        if (!rayHit.collider.gameObject.TryGetComponent(out Square square)) return;
        if (square.hasFlag) return;
        if (!_mineRoomManager.AfterFirstMove)
        {
            _mineRoomManager.SetLogic(square);
        }

        _mineRoomManager.RevealTile(square);
        
        _mineRoomManager.AfterActionFunction();
    }
    
    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if(!rayHit.collider) return;

        if (!rayHit.collider.gameObject.TryGetComponent(out Square square)) return;
        if (!square.squareRevealed)
        {
            square.hasFlag = !square.hasFlag;
        }
    }
}
