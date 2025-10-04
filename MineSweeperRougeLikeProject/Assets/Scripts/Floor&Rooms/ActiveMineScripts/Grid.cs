using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Grid : MonoBehaviour, IInteractable
{
    [SerializeField] public GameObject SquareGameObject;
    
    public int squaresXSize;
    public int squaresYSize;
    public float margin;
    public float squareLength;

    public List<SquareMine> squares;
    
    public Sprite[] spriteNumbers;

    public EndRoomScreen endRoomScreen;

    private BoxCollider2D _collider2D;

    [Header("Zoom")]
    [SerializeField] float zoomStep = 1.1f;   // >1 per scrollsteg
    [SerializeField] float minScale = 0.5f;
    [SerializeField] float maxScale = 3f;
    
    Vector3 startPos;
    Vector3 startScale;

    private FloorZoom _floorZoom;

    void Awake()
    {
        startPos   = transform.position;
        startScale = transform.localScale; // antas uniform
        _floorZoom = GetComponent<FloorZoom>();
    }

    private void Start()
    {
        if (RunPlayerStats.Instance.DebugMode) SetupGrid();
    }

    public void SetupGrid()
    {
        for (int i = 0; i < squaresXSize; i++)
        {
            for (int j = 0; j < squaresYSize; j++)
            {
                GameObject square = Instantiate(SquareGameObject, new Vector2(setOnGrid(i,squaresXSize), setOnGrid(j,squaresYSize)), quaternion.identity, transform.GetChild(0).gameObject.transform);
                SquareMine squareInfo = square.GetComponent<SquareMine>();
                squares.Add(squareInfo);
                squareInfo.position = new Vector2(i, j);
                squareInfo.Numbers = spriteNumbers;
                //squareInfo.squareRevealed = true;
                square.name = $"Square {squareInfo.position.x},{squareInfo.position.y}";
            }
        }
        _collider2D = GetComponent<BoxCollider2D>();
        _collider2D.size = new Vector2(squaresXSize*squareLength + (squaresXSize-1)*margin,squaresYSize*squareLength+ (squaresYSize-1)*margin);

    }

    public void CheckWin()
    {
        if (squares.Any(x => !x.hasMine && !x.squareRevealed)) return;
        
        RunPlayerStats.Instance.Win();
        
        //ends the round
        endRoomScreen.SetScreen(true);
    }

    float setOnGrid(int index, int squaresSize)
    {
        if (squaresSize%2==0)
        {
            return (index + index * margin) - (((squaresSize - 1) / 2) + margin * ((squaresSize - 1) / 2)) -
                   squareLength / 2 - margin / 2;
        }
        
        return (index + index * margin) - (((squaresSize - 1) / 2) + margin * ((squaresSize - 1) / 2));
    }
    
    
    public void Scroll(float value)
    {
        if (Mathf.Approximately(value, 0f)) return;

        // 1) Världen under musen före zoom
        Vector2 mouse = Mouse.current.position.ReadValue();
        Vector3 worldBefore = Camera.main.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, Camera.main.WorldToScreenPoint(transform.position).z));

        // 2) Samma punkt i lokala coords före zoom
        Vector3 localPoint = transform.InverseTransformPoint(worldBefore);

        // 3) Bestäm ny uniform skala
        float current = transform.localScale.x;
        float factor = (value > 0f) ? zoomStep : (1f / zoomStep);
        float target = Mathf.Clamp(current * factor, minScale, maxScale);
        float uniform = target / current;
        transform.localScale *= uniform;

        // 4) Världen för samma lokala punkt efter zoom
        Vector3 worldAfter = transform.TransformPoint(localPoint);

        // 5) Kompensera position så muspunkten står still
        Vector3 offset = worldBefore - worldAfter;
        transform.position += offset;
    }

    public void WheelButton()
    {
        transform.position = startPos;
        transform.localScale = startScale;
    }
}
