using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldStuffToKeep : MonoBehaviour
{
    [Header("Setup")]
    [Tooltip("Sätt denna till ett empty GameObject placerat vid dörrens gångjärn (kanten).")]
    public Transform hingePivot;

    [Header("Motion")]
    [SerializeField] private float openAngle = 90f;   // hur mycket dörren öppnar
    [SerializeField] private float durationSwing = 1f;   // tid för öppna/stäng
    [SerializeField] private int direction = 1;       // 1 = öppna 'utåt', -1 = åt andra hållet

    private bool isOpen = false;
    private bool isAnimating = false;
    
    private float zoomStop = 20f;
    private float durationZoom = 1f;
    private float durationMove = 1f;
    private float duration = 1f;
    
    
    private IEnumerator Zoom(Vector3 position, Transform grid)
    {
        float elapsed = 0f;
        while (elapsed < durationZoom)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / durationZoom);
            // 1) Världen under musen före zoom
            Vector3 worldBefore = position;
            // 2) Samma punkt i lokala coords före zoom
            Vector3 localPoint = grid.InverseTransformPoint(worldBefore);

            // 3) Bestäm ny uniform skala
            grid.localScale = Vector3.one * Mathf.Lerp(1, zoomStop, t);

            // 4) Världen för samma lokala punkt efter zoom
            Vector3 worldAfter = grid.TransformPoint(localPoint);

            // 5) Kompensera position så muspunkten står still
            Vector3 offset = worldBefore - worldAfter;
            grid.position += offset;
            yield return null;
        }
        
        //Stops now?
    }
    
    private IEnumerator SwingCoroutine(int openSign)
    {
        if (hingePivot == null)
        {
            Debug.LogWarning("DoorHingeSwing: hingePivot saknas.");
            yield break;
        }

        isAnimating = true;

        float target = direction * openSign * openAngle; // positivt eller negativt beroende på håll
        float elapsed = 0f;
        float applied = 0f; // hur många grader vi redan har roterat denna cykel

        // Rotera runt gångjärnet i små steg med RotateAround
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            // Luta kurvan lite för mjuk start/stopp
            float eased = EaseInOutCubic(t);

            float desired = Mathf.Lerp(0f, target, eased);
            float step = desired - applied;
            applied = desired;

            transform.RotateAround(
                hingePivot.position,
                Vector3.up,   // Y-axel för 3D-känsla i 2D-scen
                step
            );

            yield return null;
        }

        // Säkerställ exakt slutläge
        float finalStep = target - applied;
        if (Mathf.Abs(finalStep) > 0.001f)
        {
            transform.RotateAround(hingePivot.position, Vector3.up, finalStep);
        }

        isOpen = !isOpen;
        isAnimating = false;
    }
    
    private IEnumerator MoveToPos(Transform startPos, Vector3 targetPos)
    {
        float elapsed = 0f;
        Vector3 startPosRef = startPos.position;
        while (elapsed < durationZoom)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / durationZoom);

            startPos.position = new Vector3(Mathf.Lerp(startPosRef.x, startPosRef.x-targetPos.x, t),Mathf.Lerp(startPosRef.y, startPosRef.y-targetPos.y, t),Mathf.Lerp(startPosRef.z, startPosRef.z-targetPos.z, t)) ;
            
            yield return null;
        }
    }
    
    private float EaseInOutCubic(float x)
    {
        return x < 0.5f ? 4f*x*x*x : 1f - Mathf.Pow(-2f*x + 2f, 3f)/2f;
    }

}
