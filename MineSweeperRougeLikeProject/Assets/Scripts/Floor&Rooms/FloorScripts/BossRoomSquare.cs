using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class BossRoomSquare : MonoBehaviour, IInteractable
{
    private GameObject containter;
    public bool squareRevealed;
    public RoomBossMine room;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _spriteRendererContainer;
    private SpriteRenderer _spriteRendererBoss;
    public bool isActive;
    public Sprite squareSpriteBossUnactive;
    public Sprite squareSpriteBossActive;
    public Sprite squareSpriteUsed;
    
    // Start is called before the first frame update
    void Start()
    {
        containter = gameObject.transform.parent.transform.GetChild(1).gameObject;
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRendererContainer = containter.GetComponent<SpriteRenderer>();
        
        _spriteRendererContainer.sprite = room.sprite;

        room.bossRoomSquare = this;

        _spriteRendererBoss = gameObject.transform.parent.transform.GetChild(3).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _spriteRenderer.sprite = squareRevealed ? squareSpriteUsed : isActive ? squareSpriteBossActive : squareSpriteBossUnactive;

        _spriteRendererContainer.sortingOrder = squareRevealed ? 1 : -1;
        
        _spriteRendererBoss.sprite = RunPlayerStats.Instance.BossModification.sprite;
    }

    public void Interact()
    {
        if(!isActive) return;
        squareRevealed = true;
        RunPlayerStats.Instance.FloorManager.currentRoom = room;
        RunPlayerStats.Instance.FloorManager._bossRoomSquare = this;
        room.SetUpRoom(RunPlayerStats.Instance.FloorManager);
        StartDoorAnimation(transform);
    }

    public void SecondInteract()
    {

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
        StartCoroutine(OpenDoorAnimation(startPos, transform.position, 1, 13.846f));
    }
    
    public void CloseDoorAnimation(Transform startPos)
    {
        StartCoroutine(CloseDoorAnimation(startPos, transform.position, -1, 1));
    }
    
    private IEnumerator OpenDoorAnimation(Transform startPos, Vector3 targetPos, int openSign, float targetScale)
    {
        isAnimating = true;
        
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

        SpawnBackground();

        
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
        

        SetBackgroundSortingOrder(10);

        // Zoom In – lås en vald punkt i världen (t.ex. gångjärnet) under zoom
        elapsed = 0f;
    
        float startScale = gameObjectBackground.transform.localScale.x;
        Vector3 startZoomPos = gameObjectBackground.transform.localPosition;
        float endScale   = targetScale;

        while (elapsed < durationZoom)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / durationZoom);
            float eased = EaseInOutCubic(t);

            gameObjectBackground.transform.localScale = Vector3.Lerp(Vector3.one * startScale, Vector3.one * endScale, eased);

            yield return null;
        }
        
        isAnimating = false;

        SceneDeterminer.Instance.LoadAddedSceneGarage();

        
        elapsed = 0f;
        while (elapsed < 0.001f)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        
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
    
        float startScale = gameObjectBackground.transform.localScale.x;
        Vector3 startZoomPos = gameObjectBackground.transform.localPosition;
        float endScale   = targetScale;

        while (elapsed < durationZoom)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / durationZoom);
            float eased = EaseInOutCubic(t);

            gameObjectBackground.transform.localScale = Vector3.Lerp(Vector3.one * startScale, Vector3.one * endScale, eased);

            yield return null;
        }

        SetBackgroundSortingOrder(-2);

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

        DestroyBackground();
        
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
                position = transform.position,
                localScale = Vector3.one*1.3f,
            }
        };
        SpriteRenderer spriteRenderer = gameObjectBackgroundSelect.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = backgroundSprite;
        spriteRenderer.sortingOrder = 3;
        GameObject gameObjectBackgroundSelectChild = new GameObject
        {
            transform =
            {
                position = transform.position,
                localScale = Vector3.one*0.5f*1.3f,
                parent = gameObjectBackgroundSelect.transform
            }
        };
        SpriteRenderer spriteRendererOBJ = gameObjectBackgroundSelectChild.AddComponent<SpriteRenderer>();
        spriteRendererOBJ.sprite = room.sprite;
        spriteRendererOBJ.sortingOrder = 4;
        gameObjectBackgroundSelect.transform.SetParent(transform.parent);
        gameObjectBackground = gameObjectBackgroundSelect;
    }

    public void SetBackgroundSortingOrder(int order)
    {
        if (gameObjectBackground != null)
        {
            SpriteRenderer spriteRenderer = gameObjectBackground.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = order;
            SpriteRenderer[] spriteRenderers = gameObjectBackground.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer spriteRendererSelect in spriteRenderers)
            {
                spriteRendererSelect.sortingOrder = order + 1;
            }
        }
    }


    public void DestroyBackground()
    {
        Destroy(gameObjectBackground);
    }
}
