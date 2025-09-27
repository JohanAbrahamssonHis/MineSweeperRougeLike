using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public interface IInteractable
{
    public void Interact() {}
    public void SecondInteract() {}
    public void HoverStart(){}
    public void Hover(){}
    public void HoverEnd(){}
    public void Scroll(float value){}
    public void WheelButton() { }
}
