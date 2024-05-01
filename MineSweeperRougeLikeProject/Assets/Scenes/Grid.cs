using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] public GameObject square;
    
    public float squaresXSize;
    public float squaresYSize;
    public float margin;

    private List<GameObject> squaresX;
    private List<GameObject> squaresY;
    
    public void Start()
    {
        for (int i = 0; i < squaresXSize; i++)
        {
            for (int j = 0; j < squaresYSize; j++)
            {
                Instantiate(square, new Vector2(i-((float)squaresXSize-1)/2, j-((float)squaresYSize-1)/2), quaternion.identity);
            }
        }
    
    
    }
    
    public void Update()
    {
    }
}
