using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleWiggle : MonoBehaviour
{
    private List<GameObject> _gameObjects;
    public float speed;
    void Start()
    {
        _gameObjects = new List<GameObject>();
        
        foreach (Transform c in GetComponentInChildren<Transform>())
        {
            _gameObjects.Add(c.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        _gameObjects.ForEach(x => x.transform.localPosition += new Vector3(0,MathF.Sin(speed*Time.deltaTime),0));
    }
}
