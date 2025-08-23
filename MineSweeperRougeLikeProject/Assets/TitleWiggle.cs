using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleWiggle : MonoBehaviour
{
    private List<GameObject> _gameObjects;
    private List<Vector3> _positions;
    public float speed;
    public float strength;
    public float delay;
    void Start()
    {
        _gameObjects = new List<GameObject>();
        _positions = new List<Vector3>();
        
        foreach (Transform c in GetComponentInChildren<Transform>())
        {
            _gameObjects.Add(c.gameObject);
            _positions.Add(c.position);
        }

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _gameObjects.Count; i++)
        {
            _gameObjects[i].transform.localPosition = _positions[i] + new Vector3(0, MathF.Sin(speed * (Time.time-i*delay))*strength, 0);
        }
    }
}
