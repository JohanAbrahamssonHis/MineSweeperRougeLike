using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class LockBar : MonoBehaviour
{
    private int maxLocks;
    private List<GameObject> _gameObjects;
    private List<SpriteRenderer> _renderers;
    public Sprite sprite;
    public Sprite unlockedSprite;
    private void Start()
    {
        RunPlayerStats.Instance.LockBar = this;
        _gameObjects = new List<GameObject>();
        _renderers = new List<SpriteRenderer>();
        
        FixLocks();
    }
    
    void Update()
    {
        for (int i = 0; i < _gameObjects.Count; i++)
        {
            _renderers[i].sprite = i >= RunPlayerStats.Instance.RoomCountCleared ? sprite : unlockedSprite;
            _gameObjects[i].transform.rotation = i >= RunPlayerStats.Instance.RoomCountCleared ? quaternion.identity : quaternion.RotateZ(Mathf.PI/16);
        }
    }

    public void FixLocks()
    {
        maxLocks = RunPlayerStats.Instance.RoomLock;

        _gameObjects.ForEach(Destroy);
        _gameObjects.Clear();
        _renderers.Clear();
        
        for (int i = 0; i < maxLocks; i++)
        {
            GameObject currentGameObject = new GameObject();
            _gameObjects.Add(currentGameObject);
            currentGameObject.transform.parent = gameObject.transform;
            currentGameObject.transform.localPosition = new Vector3(i, 0, 0);
            SpriteRenderer currentRenderer = currentGameObject.AddComponent<SpriteRenderer>();
            currentRenderer.sprite = sprite;
            _renderers.Add(currentRenderer);
        }
    }
}
