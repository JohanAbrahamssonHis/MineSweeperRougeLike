using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public enum PlacementSetting
{
    Center,
    TopLeft,
    TopCenter,
    TopRight,
    CenterLeft,
    CenterRight,
    BottomLeft,
    BottomCenter,
    BottomRight
}

public enum ValueSetting
{
    Coordinate,
    Procent
}

public class ScreenRelativePlacer : MonoBehaviour
{
    private Camera Camera => Camera.main;
    public float ScreenHeigthHalf => Camera.orthographicSize;
    public float ScreenWidthHalf => ScreenHeigthHalf * Camera.aspect;

    public PlacementSetting currentPlacementSetting = PlacementSetting.Center;
    public ValueSetting currentValueSetting = ValueSetting.Coordinate;

    public float xDisplacement;
    public float yDisplacement;

    private Transform setTransform;

    public void OnEnable()
    {
        setTransform = transform;
    }

    public void Update()
    {
        SetPosition();
    }

    public void SetPosition()
    {
        switch (currentPlacementSetting)
        {
            case PlacementSetting.Center:
                setTransform.position = new Vector2(xDisplacement,yDisplacement);
                break;
            case PlacementSetting.CenterLeft:
                setTransform.position = new Vector2(-ScreenWidthHalf+xDisplacement,yDisplacement);
                break;
            case PlacementSetting.CenterRight:
                setTransform.position = new Vector2(ScreenWidthHalf+xDisplacement,yDisplacement);
                break;
            case PlacementSetting.TopCenter:
                setTransform.position = new Vector2(xDisplacement,ScreenHeigthHalf+yDisplacement);
                break;
            case PlacementSetting.TopLeft:
                setTransform.position = new Vector2(-ScreenWidthHalf+xDisplacement,ScreenHeigthHalf+yDisplacement);
                break;
            case PlacementSetting.TopRight:
                setTransform.position = new Vector2(ScreenWidthHalf+xDisplacement,ScreenHeigthHalf+yDisplacement);
                break;
            case PlacementSetting.BottomCenter:
                setTransform.position = new Vector2(xDisplacement,-ScreenHeigthHalf+yDisplacement);
                break;
            case PlacementSetting.BottomLeft:
                setTransform.position = new Vector2(-ScreenWidthHalf+xDisplacement,-ScreenHeigthHalf+yDisplacement);
                break;
            case PlacementSetting.BottomRight:
                setTransform.position = new Vector2(ScreenWidthHalf+xDisplacement,-ScreenHeigthHalf+yDisplacement);
                break;
        }
    }
}
