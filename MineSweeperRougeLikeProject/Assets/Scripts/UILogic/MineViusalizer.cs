using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class MineViusalizer : MonoBehaviour
{
    public GameObject visualObject;
   
    public List<Sprite> sprites;
    
    List<GameObject> _gameObjects = new List<GameObject>();
    
    void OnEnable()
    {
        RunPlayerStats.Instance.mineViusalizer = this;
        //RunPlayerStats.Instance.AddMalwarePackage(malwarePackage);
    }

    

    public void SetVisualizer()
    {

        List<Mine> mines = new List<Mine>();

        RunPlayerStats.Instance.MalwarePackages.ForEach(x => x.mines.ForEach( mine =>mines.Add(mine)));

        var orderedEnumerable = mines.OrderBy(mine => mine.name).ToList();

        mines = orderedEnumerable;
        
        /*
        foreach (Transform o in transform)
        {
            Destroy(o);
        }
        */
        _gameObjects.ForEach(x => Destroy(x.gameObject));
        _gameObjects.Clear();

        CreateListOfVisualHolders(mines);
    }

    private void CreateListOfVisualHolders(List<Mine> mines)
    {
        
        int amountOfMines = 1;
        
        for (var i = 0; i < mines.Count-1; i++)
        {
            if (mines[i].GetType() == mines[i + 1].GetType())
            {
                amountOfMines++;
                continue;
            }
           
            _gameObjects.Add(CreateVisualHolder(mines[i].sprite, NumberSprites.Instance.GetNumberedSprite(amountOfMines) , mines[i]));
            amountOfMines = 1;

        }
        
        //Last is created regardless
        _gameObjects.Add(CreateVisualHolder(mines.Last().sprite, NumberSprites.Instance.GetNumberedSprite(amountOfMines), mines.Last()));

        float completelength = 7;

        if (_gameObjects.Count==1)
        {
            _gameObjects.First().transform.localPosition = 
                new Vector3(0,  0, 0);
            return;
        }
        
        for (var i = 0; i < _gameObjects.Count; i++)
        {
            _gameObjects[i].transform.localPosition = 
                new Vector3(0, i * -(completelength/(_gameObjects.Count-1)) + completelength/2, 0);
        }
    }

    private GameObject CreateVisualHolder(Sprite mineSprite, Sprite numberSprite, Mine mine)
    {
        GameObject currentGameObject = Instantiate(visualObject, gameObject.transform);
        
        currentGameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = mineSprite;
        currentGameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = numberSprite;
        
        currentGameObject.GetComponent<MineVisualHolder>().mine = mine;

        return currentGameObject;
    }
}
