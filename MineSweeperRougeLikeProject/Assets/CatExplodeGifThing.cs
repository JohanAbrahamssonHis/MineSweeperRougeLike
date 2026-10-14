using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatExplodeGifThing : MonoBehaviour
{
    private bool CatIsReal;
    private float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        if(UnityEngine.Random.Range(0,100)==0) CatIsReal = true;
        else gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!CatIsReal) return;

        currentTime += Time.deltaTime;

        if(currentTime>=2)

        if (Application.isEditor) UnityEditor.EditorApplication.ExitPlaymode();
        else Application.Quit();
    }
}
