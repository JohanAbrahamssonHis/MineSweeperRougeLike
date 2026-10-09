using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RemoveTimeSetting : MonoBehaviour, IInteractable, ITextable
{
    int healthOn = 3;
    int healthOff = 5;
    public SpriteRenderer spriteRenderer;
    public Sprite spriteOn;
    public Sprite spriteOff;

    public SpriteRenderer spriteRendererClock;

    public string Name => "No Timer Setting";

    public string Description => "Removes the timer and all other direct time related objects. However you start with only "+healthOn+" health ";

    public void Start()
    {
        StartData.Instance.Health = StartData.Instance.removeTimeValues ? healthOn : healthOff;
        spriteRenderer.sprite = StartData.Instance.removeTimeValues ? spriteOn: spriteOff;
        spriteRendererClock.color = StartData.Instance.removeTimeValues ? Color.white: Color.gray;
    }

    public void Interact()
    {
        StartData.Instance.removeTimeValues = !StartData.Instance.removeTimeValues;
        StartData.Instance.Health = StartData.Instance.removeTimeValues ? healthOn : healthOff;
        spriteRenderer.sprite = StartData.Instance.removeTimeValues ? spriteOn: spriteOff;
        spriteRendererClock.color = StartData.Instance.removeTimeValues ? Color.white: Color.gray;
    }
}
