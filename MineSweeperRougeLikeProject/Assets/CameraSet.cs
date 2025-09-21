using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraSet : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    // Start is called before the first frame update
    void Awake()
    {
        
        if (RunPlayerStats.Instance.Camera != null)
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(this.gameObject);
        RunPlayerStats.Instance.Camera = camera;
        
    }

    public void OnAppLaunch()
    {
        Debug.Log("HEllos");
        RunPlayerStats.Instance.Camera = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
