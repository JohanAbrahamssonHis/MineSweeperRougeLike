using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector3 = UnityEngine.Vector3;

public class FloorZoom : MonoBehaviour
{
    private const float zoomStop = 20f;
    private const float duration = 1f;
    
    public static IEnumerator Zoom(Vector3 position, Grid grid)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            // 1) Världen under musen före zoom
            Vector3 worldBefore = position;
            // 2) Samma punkt i lokala coords före zoom
            Vector3 localPoint = grid.transform.InverseTransformPoint(worldBefore);

            // 3) Bestäm ny uniform skala
            grid.transform.localScale = Vector3.one * Mathf.Lerp(1, zoomStop, t);

            // 4) Världen för samma lokala punkt efter zoom
            Vector3 worldAfter = grid.transform.TransformPoint(localPoint);

            // 5) Kompensera position så muspunkten står still
            Vector3 offset = worldBefore - worldAfter;
            grid.transform.position += offset;
            yield return null;
        }
        
        //Stops now?
    }
}
