using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private int curentHealth; // Tracks current health
    private List<GameObject> _gameObjects; // List of health icon GameObjects
    public Sprite sprite; // Sprite for health icons
    public float distance; // Spacing between icons


    private List<Vector3> _positions;

    public float speed;
    public float delay;
    public float strength;
    
    private void OnEnable()
    {
        RunPlayerStats.Instance.HealthBar = this;
        curentHealth = RunPlayerStats.Instance.Health;
        _gameObjects = new List<GameObject>();
        _positions = new List<Vector3>();
        // Create health icons
        for (int i = 0; i < curentHealth; i++)
        {
            GameObject currentGameObject = new GameObject($"Health_{i}");
            _gameObjects.Add(currentGameObject);
            _positions.Add(Vector3.zero);
            currentGameObject.transform.parent = gameObject.transform;
            SpriteRenderer _renderer = currentGameObject.AddComponent<SpriteRenderer>();
            _renderer.sprite = sprite;
        }
        UpdateIconPositions();
    }

    public void Update()
    {
        for (int i = 0; i < _gameObjects.Count; i++)
        {
            _gameObjects[i].transform.localPosition = _positions[i] + new Vector3(0, MathF.Sin(speed * (Time.time-i*delay))*strength, 0);
        }
    }

    public void HealthChanged(int health)
    {
        if (RunPlayerStats.Instance.HealthBar == null) return;
        if (health > curentHealth) HealthAdded(health - curentHealth);
        else if (health < curentHealth) HealthRemoved(curentHealth - health);
        UpdateIconPositions();
        curentHealth = health;
    }

    private void HealthAdded(int healthAdded)
    {
        for (int i = 0; i < healthAdded; i++)
        {
            GameObject currentGameObject = new GameObject($"Health_{_gameObjects.Count}");
            _gameObjects.Add(currentGameObject);
            _positions.Add(Vector3.zero);
            currentGameObject.transform.parent = gameObject.transform;
            SpriteRenderer _renderer = currentGameObject.AddComponent<SpriteRenderer>();
            _renderer.sprite = sprite;
        }
    }

    private void HealthRemoved(int healthRemoved)
    {
        for (int i = 0; i < healthRemoved; i++)
        {
            int lastIndex = _gameObjects.Count - 1;
            if (lastIndex >= 0)
            {
                Destroy(_gameObjects[lastIndex]);
                _gameObjects.RemoveAt(lastIndex);
                _positions.RemoveAt(lastIndex);
            }
        }
    }

    // Helper to center and space icons so they fill the total area defined by distance
    private void UpdateIconPositions()
    {
        // If only one icon, place it at the center
        if (_gameObjects.Count == 1)
        {
            _gameObjects[0].transform.localPosition = Vector3.zero;
            return;
        }
        // Calculate the total width to fill
        float totalWidth = distance;
        // Calculate spacing so icons fill the area from -distance/2 to +distance/2
        // +1 instead of -1 to include spacing on both ends
        float iconSpacing = totalWidth / (_gameObjects.Count + 1);
        float startX = -totalWidth / 2f;
        for (int i = 0; i < _gameObjects.Count; i++)
        {
            float x = startX + (i + 1) * iconSpacing;
            _gameObjects[i].transform.localPosition = new Vector3(x, 0, 0);
            _positions[i] = new Vector3(x, 0, 0);
        }
    }
}
