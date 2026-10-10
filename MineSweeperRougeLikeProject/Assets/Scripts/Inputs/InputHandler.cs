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

    public Vector2 MousePositionOffset;
    public Vector2 MousePosition {
        get => Mouse.current.position.ReadValue()+MousePositionOffset;
        }

    public bool isNotSwaped = true;

    public Texture2D cursorText;

    [Header("Assign your cursor sprite here")]
    public RectTransform cursorImage; // UI Image or RectTransform for the fake cursor
    public Canvas canvas;             // The canvas containing the cursor

    [Header("Cursor Settings")]
    public bool lockSystemCursor = true; // Hide and lock system cursor
    public Vector2 customPosition;       // Position to set the fake cursor
    
    void Start()
    {
        _mainCamera = RunPlayerStats.Instance.Camera;
        _currentlyInteracted = new List<IInteractable>();
        RunPlayerStats.Instance.InputHandler = this;
        isNotSwaped = true;
        //Cursor.SetCursor(cursorText, MousePositionOffset, CursorMode.Auto);

        if (lockSystemCursor)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined; // Or Locked if you want fixed position
        }
    }
    
    
    public void OnClick(InputAction.CallbackContext context)
    {
        if(isNotSwaped) ButtonEffect(context)?.ForEach(x => x.Interact());
        else ButtonEffect(context)?.ForEach(x => x.SecondInteract());
    }
    
    public void OnRightClick(InputAction.CallbackContext context)
    {

        if(isNotSwaped) ButtonEffect(context)?.ForEach(x => x.SecondInteract());
        else ButtonEffect(context)?.ForEach(x => x.Interact());
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

        //TODO: this is bad but will work for now
        if (lockSystemCursor)
        {
            Cursor.visible = false;
        }

        Vector2 mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            (Vector2)Input.mousePosition,
            canvas.worldCamera,
            out mousePos
        );
        cursorImage.localPosition = mousePos;
    }

    private List<IInteractable> ButtonEffect(InputAction.CallbackContext context)
    {
        if (!context.performed) return null;
        List<IInteractable> interactables = new List<IInteractable>();
        var rayHits = Physics2D.GetRayIntersectionAll(_mainCamera.ScreenPointToRay(MousePosition));
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
        var rayHits = Physics2D.GetRayIntersectionAll(_mainCamera.ScreenPointToRay(MousePosition));
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
                if(!RunPlayerStats.Instance.EndState) interactable?.HoverStart();
                _currentlyInteracted.Add(interactable);
            }
            interactables.Add(interactable);
        }

        foreach (var interactable in _currentlyInteracted.ToList().Where(interactable => !interactables.Contains(interactable)))
        {
            if(interactable is null) continue;
            if(!RunPlayerStats.Instance.EndState) interactable?.HoverEnd();
            if (interactable == _mostCurrentlyInteracted)
            {
                _mostCurrentlyInteracted = null;
                TextVisualSingleton.Instance.textVisualObject.DisableObject();
            }
            _currentlyInteracted.Remove(interactable);
        }

        return interactables;
    }
    

    public void SetCursor(Vector2 vector2)
    {
        Cursor.SetCursor(cursorText, vector2, CursorMode.Auto);
    }

    public void SetFakeCursorPosition(Vector2 screenPosition)
    {
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            screenPosition + (Vector2)Input.mousePosition,
            canvas.worldCamera,
            out localPos
        );
        cursorImage.localPosition = localPos;
    }

    // - new Vector2(-cursorImage.rect.width/2,cursorImage.rect.height/2),
}
