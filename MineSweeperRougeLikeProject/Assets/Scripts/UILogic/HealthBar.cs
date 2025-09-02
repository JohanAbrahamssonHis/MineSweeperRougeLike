using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private int curentHealth;
    private List<GameObject> _gameObjects;
    public Sprite sprite;
    public float distance;
    private void OnEnable()
    {
        RunPlayerStats.Instance.HealthBar = this;
        
        curentHealth = RunPlayerStats.Instance.Health;

        _gameObjects = new List<GameObject>();

        for (int i = 1; i <= curentHealth; i++)
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
        /*
        for (int i = 0; i < curentHealth; i++)
        {
            _gameObjects[i].SetActive(i<RunPlayerStats.Instance.Health);
        }
        */
    }

    public void HealthChanged(int health)
    {
        if(RunPlayerStats.Instance.HealthBar==null) return;
        
        if (health > curentHealth) HealthAdded(health-curentHealth);
        
        else if (health < curentHealth) HealthRemoved(curentHealth-health);
        
        for (int i = 0; i < _gameObjects.Count; i++)
            _gameObjects[i].transform.localPosition = new
                Vector3((i - (float)_gameObjects.Count / 2)*distance*(1/(float)_gameObjects.Count),
                    0, 0);

        curentHealth = RunPlayerStats.Instance.Health;
    }

    private void HealthAdded(int healthAdded)
    {
        
        for (int i = 1; i <= healthAdded; i++)
        {
            GameObject currentGameObject = new GameObject();
            _gameObjects.Add(currentGameObject);
            currentGameObject.transform.parent = gameObject.transform;
            SpriteRenderer _renderer = currentGameObject.AddComponent<SpriteRenderer>();
            _renderer.sprite = sprite;
        }
    }

    private void HealthRemoved(int healthRemoved)
    {
        
        for (int i = healthRemoved - 1; i >= 0; i--)
        {
            Destroy(_gameObjects[i]);
            _gameObjects.RemoveAt(i);
        }
    }
}
