using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private int maxHealth;
    private List<GameObject> _gameObjects;
    public Sprite sprite;
    private void Start()
    {
        maxHealth = RunPlayerStats.Instance.Health;

        _gameObjects = new List<GameObject>();

        for (int i = 0; i < maxHealth; i++)
        {
            GameObject currentGameObject = new GameObject();
            _gameObjects.Add(currentGameObject);
            currentGameObject.transform.parent = gameObject.transform;
            currentGameObject.transform.localPosition = new Vector3(i, 0, 0);
            SpriteRenderer _renderer = currentGameObject.AddComponent<SpriteRenderer>();
            _renderer.sprite = sprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            _gameObjects[i].SetActive(i<RunPlayerStats.Instance.Health);
        }
    }
}
