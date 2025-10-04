using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputHandler : MonoBehaviour
{
    private Camera _mainCamera;
    private List<IInteractable> _currentlyInteracted;
    private IInteractable _mostCurrentlyInteracted;
    
    void Start()
    {
        _mainCamera = RunPlayerStats.Instance.Camera;
        _currentlyInteracted = new List<IInteractable>();
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

    public void Update()
    {
        ButtonEffectHover()?.ForEach(x => x.Hover());
    }

    private List<IInteractable> ButtonEffect(InputAction.CallbackContext context)
    {
        if (!context.performed) return null;
        List<IInteractable> interactables = new List<IInteractable>();
        var rayHits = Physics2D.GetRayIntersectionAll(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if(rayHits.Any(x => !x.collider)) return null;

        foreach (var rayHit in rayHits)
        {
            if (!rayHit.collider.gameObject.TryGetComponent(out IInteractable interactable)) continue;
            interactables.Add(interactable);
        }

        return interactables;
    }
    
    private List<IInteractable> ButtonEffectHover()
    {
        List<IInteractable> interactables = new List<IInteractable>();
        var rayHits = Physics2D.GetRayIntersectionAll(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if(rayHits.Any(x => !x.collider)) return null;

        foreach (var rayHit in rayHits)
        {
            if (!rayHit.collider.gameObject.TryGetComponent(out IInteractable interactable)) continue;

            if (rayHit.collider.gameObject.TryGetComponent(out ITextable textable))
            {
                if (_mostCurrentlyInteracted == null)
                {
                    _mostCurrentlyInteracted = interactable;
                    TextVisualSingleton.Instance.textVisualObject.SetObject(rayHit.collider.gameObject, textable);
                }
            }
            
            if (!_currentlyInteracted.Contains(interactable))
            {
                interactable.HoverStart();
                _currentlyInteracted.Add(interactable);
            }
            interactables.Add(interactable);
        }

        foreach (var interactable in _currentlyInteracted.ToList().Where(interactable => !interactables.Contains(interactable)))
        {
            interactable?.HoverEnd();
            if (interactable == _mostCurrentlyInteracted)
            {
                _mostCurrentlyInteracted = null;
                TextVisualSingleton.Instance.textVisualObject.DisableObject();
            }
            _currentlyInteracted.Remove(interactable);
        }

        return interactables;
    }
    
}
