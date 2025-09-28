using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraSet : MonoBehaviour
{
    [SerializeField] private Camera camera;

    private void Awake()
    {
        var stats = RunPlayerStats.Instance;

        // Finns redan en kamera som inte är denna? Döda denna.
        if (stats.Camera != null && stats.Camera != camera)
        {
            Destroy(gameObject);
            return;
        }

        // Sätt och bevara
        stats.Camera = camera != null ? camera : GetComponentInChildren<Camera>();
        DontDestroyOnLoad(gameObject);
    }

    // Bra att nollställa när detta objekt försvinner
    private void OnDestroy()
    {
        if (RunPlayerStats.Instance != null && RunPlayerStats.Instance.Camera == camera)
            RunPlayerStats.Instance.Camera = null;
    }

    private void OnApplicationQuit()
    {
        // Säkerhet – påverkar inte Editor-asset längre då fältet ej serialiseras
        if (RunPlayerStats.Instance != null && RunPlayerStats.Instance.Camera == camera)
            RunPlayerStats.Instance.Camera = null;
    }
}
