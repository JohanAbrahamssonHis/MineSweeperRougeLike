using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SquareFloor : MonoBehaviour, IInteractable
{
    public int number;
    private GameObject containter;
    public bool squareRevealed;
    public bool hasRoom;
    public Room room;
    public Sprite[] Numbers;
    public Vector2 position;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _spriteRendererContainer;
    public bool hasNeighbourRoom;

    public bool hasFlag;
    private GameObject flagContainer;
    private SpriteRenderer _spriteRendererFlagContainer;

    public bool hasNeighbourShop;
    public Sprite decalSprite;
    private GameObject decalContainer;
    private SpriteRenderer _spriteRendererDecalContainer;

    public Sprite squareSpriteUnused;
    public Sprite squareSpriteUsed;
    
    // Start is called before the first frame update
    void Start()
    {
        containter = gameObject.transform.GetChild(0).gameObject;
        flagContainer = gameObject.transform.GetChild(1).gameObject;
        decalContainer = gameObject.transform.GetChild(2).gameObject;
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRendererContainer = containter.GetComponent<SpriteRenderer>();
        _spriteRendererFlagContainer = flagContainer.GetComponent<SpriteRenderer>();
        _spriteRendererDecalContainer = decalContainer.GetComponent<SpriteRenderer>();

        _spriteRendererDecalContainer.sprite = decalSprite;
    }

    // Update is called once per frame
    void Update()
    {
        _spriteRenderer.sprite = squareRevealed ? squareSpriteUsed : squareSpriteUnused;
        
        _spriteRendererContainer.sprite = hasNeighbourRoom ? hasRoom ? room.sprite : NumberSprites.Instance.GetNumberedSprite(number) : null;

        _spriteRendererContainer.sortingOrder = squareRevealed ? 1 : -1;
        
        _spriteRendererFlagContainer.gameObject.SetActive(hasFlag);
        
        decalContainer.SetActive(hasNeighbourShop && squareRevealed);
    }

    public void Interact()
    {
        if(hasFlag || squareRevealed) return;
        
        FloorManager floorManager = RunPlayerStats.Instance.FloorManager;
        
        SoundManager.Instance.Play("Action", null, true, 1);
        
        if (!floorManager.AfterFirstMove)
            floorManager.SetLogic(this);
        else
        {
            if (squareRevealed) return;
            floorManager.RevealTile(this);
            
            floorManager.AfterActionFunction();
        }
    }

    public void SecondInteract()
    {
        if (squareRevealed) return;
        hasFlag = !hasFlag;
        
        SoundManager.Instance.Play("Flag", transform, true, 1);
    }
    
    private float zoomStop = 20f;
    [SerializeField] private float durationZoom = 1f;
    [SerializeField] private float durationMove = 1f;
    [SerializeField] private float duration = 1.0f;

    [Header("Setup")]
    [Tooltip("Sätt denna till ett empty GameObject placerat vid dörrens gångjärn (kanten).")]
    public Transform hingePivot;

    [Header("Motion")]
    [SerializeField] private float openAngle = 90f;   // hur mycket dörren öppnar
    [SerializeField] private float durationSwing = 1f;   // tid för öppna/stäng
    [SerializeField] private int direction = 1;       // 1 = öppna 'utåt', -1 = åt andra hållet

    private bool isOpen = false;
    private bool isAnimating = false;
    [SerializeField] private Sprite backgroundSprite;

    public void StartDoorAnimation(Transform startPos)
    {
        StartCoroutine(OpenDoorAnimation(startPos, transform.position, 1, 18));
    }
    
    public void CloseDoorAnimation(Transform startPos)
    {
        StartCoroutine(CloseDoorAnimation(startPos, transform.position, -1, 1));
    }
    
    private IEnumerator OpenDoorAnimation(Transform startPos, Vector3 targetPos, int openSign, float targetScale)
    {
        isAnimating = true;

        SpawnBackground();
        
        //Move to position
        float elapsed = 0f;
        Vector3 startPosRef = startPos.position;
        while (elapsed < durationMove)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / durationMove);
            float eased = EaseInOutCubic(t);

            startPos.position = new Vector3(Mathf.Lerp(startPosRef.x, startPosRef.x - targetPos.x, eased),
                Mathf.Lerp(startPosRef.y, startPosRef.y - targetPos.y, eased),
                Mathf.Lerp(startPosRef.z, startPosRef.z - targetPos.z, eased));

            yield return null;
        }

        //Door Swings
        if (hingePivot == null)
        {
            Debug.LogWarning("DoorHingeSwing: hingePivot saknas.");
            yield break;
        }

        float target = direction * openSign * openAngle; // positivt eller negativt beroende på håll
        elapsed = 0f;
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
                Vector3.up, // Y-axel för 3D-känsla i 2D-scen
                step
            );

            yield return null;
        }
        
        // Zoom In – lås en vald punkt i världen (t.ex. gångjärnet) under zoom
        elapsed = 0f;
    
        float startScale = startPos.localScale.x;
        Vector3 startZoomPos = startPos.localPosition;
        float endScale   = targetScale;

        while (elapsed < durationZoom)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / durationZoom);
            float eased = EaseInOutCubic(t);

            startPos.localPosition = new Vector3(Mathf.Lerp(startZoomPos.x, -targetPos.x*endScale, eased),
                Mathf.Lerp(startZoomPos.y, -targetPos.y*endScale, eased),
                Mathf.Lerp(startZoomPos.z, -targetPos.z*endScale, eased));
            
            startPos.localScale = new Vector3(Mathf.Lerp(startScale, endScale, eased),
                Mathf.Lerp(startScale, endScale, eased),
                Mathf.Lerp(startScale, endScale, eased));

            yield return null;
        }
        
        isAnimating = false;
        
        room.RoomFunction();

        test = targetPos;
    }

    private Vector3 test;
    
    private IEnumerator CloseDoorAnimation(Transform startPos, Vector3 targetPos, int openSign, float targetScale)
    {
        targetPos = test;
        
        isAnimating = true;
        
        // Zoom In – lås en vald punkt i världen (t.ex. gångjärnet) under zoom
        float elapsed = 0f;
    
        float startScale = startPos.localScale.x;
        Vector3 startZoomPos = startPos.localPosition;
        float endScale   = targetScale;

        while (elapsed < durationZoom)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / durationZoom);
            float eased = EaseInOutCubic(t);

            startPos.localPosition = Vector3.Lerp(startZoomPos, -targetPos * endScale, eased);

            startPos.localScale = Vector3.Lerp(Vector3.one*startScale,Vector3.one*endScale, eased);

            yield return null;
        }

        //Door Swings
        if (hingePivot == null)
        {
            Debug.LogWarning("DoorHingeSwing: hingePivot saknas.");
            yield break;
        }

        float target = direction * openSign * openAngle; // positivt eller negativt beroende på håll
        elapsed = 0f;
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
                Vector3.up, // Y-axel för 3D-känsla i 2D-scen
                step
            );

            yield return null;
        }
        
        
        //Move to position
        elapsed = 0f;
        Vector3 startPosRef = startPos.position;
        while (elapsed < durationMove)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / durationMove);
            float eased = EaseInOutCubic(t);

            startPos.position = Vector3.Lerp(startPosRef, startPosRef+targetPos, eased);

            yield return null;
        }

        DestroyBackground();
    }
    
    private float EaseInOutCubic(float x)
    {
        return x < 0.5f ? 4f*x*x*x : 1f - Mathf.Pow(-2f*x + 2f, 3f)/2f;
    }

    private GameObject gameObjectBackground;
    public void SpawnBackground()
    {
        GameObject gameObjectBackgroundSelect = new GameObject
        {
            transform =
            {
                position = transform.position
            }
        };
        SpriteRenderer spriteRenderer = gameObjectBackgroundSelect.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = backgroundSprite;
        spriteRenderer.sortingOrder = -2;
        GameObject gameObjectBackgroundSelectChild = new GameObject
        {
            transform =
            {
                position = transform.position,
                localScale = Vector3.one*0.5f,
                parent = gameObjectBackgroundSelect.transform
            }
        };
        SpriteRenderer spriteRendererOBJ = gameObjectBackgroundSelectChild.AddComponent<SpriteRenderer>();
        spriteRendererOBJ.sprite = room.sprite;
        spriteRendererOBJ.sortingOrder = -1;
        gameObjectBackgroundSelect.transform.SetParent(transform.parent);
        gameObjectBackground = gameObjectBackgroundSelect;
    }
    
    public void DestroyBackground()
    {
        Destroy(gameObjectBackground);
    }
    
}