using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputFloorHandler : MonoBehaviour
{
    private Camera _mainCamera;
    public FloorManager floorManager;
    
    void Awake()
    {
        _mainCamera = Camera.main;
    }
    
    
    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if(!rayHit.collider) return;

        if (rayHit.collider.gameObject.TryGetComponent(out SquareFloor square))
        {
            if (!floorManager.AfterFirstMove)
                floorManager.SetLogic(square);
            else
            {
                if (square.squareRevealed) return;
                floorManager.RevealTile(square);

                floorManager.AfterActionFunction();
            }
        }
        else if (rayHit.collider.gameObject.TryGetComponent(out BossRoomSquare bossSquare))
        {
            if(!bossSquare.isActive) return;
            bossSquare.squareRevealed = true;
            floorManager.currentRoom = bossSquare.room;
            bossSquare.room.RoomFunction();
        }
    }
    
    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if(!rayHit.collider) return;

        if (!rayHit.collider.gameObject.TryGetComponent(out SquareMine square)) return;
        if (!square.squareRevealed)
        {
            square.hasFlag = !square.hasFlag;
        }
    }
    
    public void OnResetBoard(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        
        floorManager.ResetBoard();
    }
}
