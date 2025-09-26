using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextVisualObject : MonoBehaviour
{
    private GameObject parentObject;
    private IInteractable parentInteract;
    private GameObject HiderContainer;
    private RectTransform HiderForm;
    private BoxCollider2D _boxCollider2D;
    public bool isStillHovered;
    void Awake()
    {
        parentObject = gameObject.transform.parent.gameObject;
        parentInteract = gameObject.GetComponentInParent<IInteractable>();
        HiderContainer = transform.GetChild(0).gameObject;
        HiderForm = HiderContainer.GetComponent<RectTransform>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _boxCollider2D.offset = new Vector2(-HiderForm.rect.x, HiderForm.rect.y);
        _boxCollider2D.size = new Vector2(HiderForm.rect.width, HiderForm.rect.height);
        HiderContainer.SetActive(false);
    }
    
    public void OnMouseEnter()
    {
        HiderContainer.SetActive(true);
        isStillHovered = true;
        //parentInteract.HoverStart();
    }

    public void OnMouseOver()
    {
        parentInteract.Hover();
    }

    public void OnMouseExit()
    {
        HiderContainer.SetActive(false);
        isStillHovered = false;
        parentInteract.HoverEnd("Sure is now not Hovering Second");
    }

    public void Nothing()
    {
        
    }
}
