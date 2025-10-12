using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using UnityEngine;

public class GarageDoorShaker : MonoBehaviour
{
    [Header("General")]
    [Tooltip("Use localPosition instead of world position")]
    [SerializeField] private bool useLocalPosition = true;
    [Tooltip("If > 0, overrides auto distance calc and moves this much up when opening")]
    [SerializeField] private float openDistanceOverride = 0f;
    [Tooltip("Extra margin (world units) above screen when auto-calculating open distance")]
    [SerializeField] private float openMargin = 0.5f;

    [Header("Timing")]
    [SerializeField] private float shakeBeforeOpenDuration = 0.25f;
    [SerializeField] private float openMoveDuration = 0.5f;
    [SerializeField] private float closeMoveDuration = 0.5f;
    [SerializeField] private float shakeAfterCloseDuration = 0.25f;

    [Header("Shake")]
    [Tooltip("Horizontal shake amplitude in units")]
    [SerializeField] private float shakeStrengthX = 0.08f;
    [Tooltip("Vertical shake amplitude in units")]
    [SerializeField] private float shakeStrengthY = 0.02f;
    [Tooltip("Set > 0 for stronger first shakes, < 0 for stronger last shakes")]
    [SerializeField] private float shakeDampBias = 0.0f; // 0 = linjär dämpning

    [Header("Easing (0..1 input)")]
    [SerializeField] private AnimationCurve easeOpen = AnimationCurve.EaseInOut(0,0,1,1);
    [SerializeField] private AnimationCurve easeClose = AnimationCurve.EaseInOut(0,0,1,1);

    private Vector3 _closedPos;     // utgångsläge (stängt)
    private Vector3 _openPos;       // beräknat öppet läge
    private Coroutine _running;
    private bool _isOpen;

    void Awake()
    {
        _closedPos = GetPos();
        _openPos = ComputeOpenPosition();
    }

    // Anropa dessa från din logik:
    public void OpenDoor()
    {
        if (_running != null) StopCoroutine(_running);
        _running = StartCoroutine(OpenSequence());
    }

    public void CloseDoor(string sceneName)
    {
        if (_running != null) StopCoroutine(_running);
        _running = StartCoroutine(CloseSequence(sceneName));
    }

    IEnumerator OpenSequence()
    {
        // liten “rassel” innan den börjar rulla upp
        yield return Shake(shakeBeforeOpenDuration);

        // flytta upp
        yield return MoveTo(_openPos, openMoveDuration, easeOpen);
        _isOpen = true;
        _running = null;
    }

    IEnumerator CloseSequence(string sceneName)
    {
        // flytta ner
        yield return MoveTo(_closedPos, closeMoveDuration, easeClose);
        _isOpen = false;

        // “impact”-skak när den slår i nederkant
        yield return Shake(shakeAfterCloseDuration);
        _running = null;

        SceneDeterminer.ReturnToFloorAfter(sceneName);
    }

    // ----- Helpers -----

    Vector3 ComputeOpenPosition()
    {
        // Manuell override
        if (openDistanceOverride > 0f)
            return _closedPos + new Vector3(0f, openDistanceOverride, 0f);

        // Auto: flytta så objektets nederkant hamnar ovanför skärmens överkant + margin
        var cam = Camera.main;
        if (cam != null && cam.orthographic)
        {
            // objektets world-bounds
            var rend = GetComponentInChildren<Renderer>();
            Bounds b;
            if (rend != null) b = rend.bounds;
            else
            {
                // fallback: anta 1 enhet hög
                b = new Bounds(transform.position, Vector3.one);
            }

            float screenTopY = cam.transform.position.y + cam.orthographicSize;
            float wantedBottomY = screenTopY + openMargin;

            float deltaY = Mathf.Max(0f, wantedBottomY - b.min.y);
            return _closedPos + new Vector3(0f, deltaY, 0f);
        }

        // fallback för perspektiv: flytta 5 enheter upp
        return _closedPos + new Vector3(0f, 5f, 0f);
    }

    Vector3 GetPos() => useLocalPosition ? transform.localPosition : transform.position;
    void SetPos(Vector3 p)
    {
        if (useLocalPosition) transform.localPosition = p;
        else transform.position = p;
    }

    IEnumerator MoveTo(Vector3 target, float duration, AnimationCurve ease)
    {
        Vector3 start = GetPos();
        float t = 0f;
        duration = Mathf.Max(0.0001f, duration);

        while (t < duration)
        {
            t += Time.deltaTime;
            float u = Mathf.Clamp01(t / duration);
            float e = ease.Evaluate(u);
            SetPos(Vector3.LerpUnclamped(start, target, e));
            yield return null;
        }
        SetPos(target);
    }

    IEnumerator Shake(float duration)
    {
        if (duration <= 0f) yield break;

        Vector3 basePos = GetPos();
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float u = Mathf.Clamp01(t / duration);

            // Dämpning över tid (1 -> 0). Bias styr kurvformen.
            float damp = 1f - u;
            if (Mathf.Abs(shakeDampBias) > 0.0001f)
                damp = Mathf.Pow(damp, 1f - Mathf.Clamp(shakeDampBias, -0.95f, 0.95f));

            // Liten jitter i X + Y (ser ut som metall som vibrerar)
            float offX = (Random.value * 2f - 1f) * shakeStrengthX * damp;
            float offY = (Random.value * 2f - 1f) * shakeStrengthY * damp;

            SetPos(basePos + new Vector3(offX, offY, 0f));
            yield return null;
        }

        // tillbaka exakt
        SetPos(basePos);
    }
/*
    // Debug-knappar i editor (valfritt)
#if UNITY_EDITOR
    [ContextMenu("Open Door")]
    void _ctxOpen() => OpenDoor();
    [ContextMenu("Close Door")]
    void _ctxClose() => CloseDoor();
    [ContextMenu("Recompute Open Pos")]
    void _ctxRecalc() => _openPos = ComputeOpenPosition();
#endif
*/
}

