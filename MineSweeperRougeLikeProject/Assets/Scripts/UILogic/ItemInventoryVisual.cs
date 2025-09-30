using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemInventoryVisual : MonoBehaviour
{
    private List<Item> inventory;
    public GameObject itemVisualPreset;
    public float distance;
    
    //Wiggle values
    private List<GameObject> _gameObjects;
    private List<Vector3> _positions;
    public float speed;
    public float strength;
    public float delay;
    void Start()
    {
        inventory = new List<Item>();
        
        _gameObjects = new List<GameObject>();
        _positions = new List<Vector3>();

        RunPlayerStats.Instance.ItemInventoryVisual = this;
        
        FixVisual();
    }

    private void Update()
    {
        for (int i = 0; i < _gameObjects.Count; i++)
        {
            _gameObjects[i].transform.localPosition = _positions[i] + new Vector3(0, MathF.Sin(speed * (Time.time-i*delay))*strength, 0);
        }
    }

    public void FixVisual()
    {
        inventory.Clear();
        
        _gameObjects.ForEach(Destroy);
        _gameObjects.Clear();
        _positions.Clear();
        
        
        
        RunPlayerStats.Instance.Inventory.ForEach(x => inventory.Add(x));
        
        if(inventory.Count == 0) return;
        for (int i = 0; i < inventory.Count; i++)
        {
            GameObject newItemGameObject = Instantiate(itemVisualPreset, transform);
            newItemGameObject.transform.localPosition = new Vector3((i - (float)inventory.Count / 2)*distance*(1/(float)inventory.Count), 0, 0);
            newItemGameObject.GetComponent<SpriteRenderer>().sprite = inventory[i].sprite;
            newItemGameObject.GetComponent<ItemVisualHover>()._item = inventory[i];
            _gameObjects.Add(newItemGameObject);
            _positions.Add(newItemGameObject.transform.localPosition);
        }
    }
}
