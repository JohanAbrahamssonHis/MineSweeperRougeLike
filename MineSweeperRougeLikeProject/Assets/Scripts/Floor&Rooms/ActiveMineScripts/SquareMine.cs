using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public enum SquareColour
{
    Grey,
    Red,
    Gold,
    Blue,
    Orange,
    Green,
    Sheep,
}

public enum SquareType
{
    Hidden,
    Revealed
}



public class SquareMine : MonoBehaviour, IInteractable
{
    public int number;
    public int longNumber;
    private GameObject containter;
    private GameObject flagContainer;
    private GameObject decalContainter;
    
    public SquareColour currentSquareColour;
    public bool squareRevealed;
    public bool hasMine;
    public Mine mine;
    public bool hasFlag;
    public Sprite[] Numbers;
    public Vector2 position;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _spriteRendererContainer;
    private SpriteRenderer _spriteRendererFlagContainer;
    private SpriteRenderer _spriteRendererDecalContainer;

    public Sprite DecalArrow;
    public bool isLongNeighbour;

    private SpriteRenderer _spriteRendererMineFlagRenderer;
    
    private SpriteRenderer _spriteRendererDecalContainerRenderer;
    
    public bool hasNeighbourMine;

    public Sprite squareSpriteUnused;
    public Sprite squareSpriteUsed;

    private bool _isHovered;
    [Header("Rotate Tilt")]
    [Range(0f, 45f)] public float maxAngle = 12f;   // max tilt i grader
    public float radius = 2.0f;            // längdskala; större = mjukare lut
    public float smooth = 15f;             // följhastighet
    
    /*
    [Header("Rotation shift")]
    [SerializeField] private float rotateAngle = 20f;   // how much to rotate
    [SerializeField] private float rotateTime = 0.1f;   // how fast rotation happens
    [SerializeField] private float holdTime = 0.2f;     // how long to stay rotated
    */
    [SerializeField] private float bobbleAngle = 20f;   // how far to tilt (degrees)
    [SerializeField] private float bobbleSpeed = 10f; // how fast per half-tilt
    [SerializeField] private float damping = 3f;
    private bool isBubbling = false;

    [SerializeField] private Material dissolveMaterial;
    
    // Start is called before the first frame update
    void Start()
    {
        containter = gameObject.transform.GetChild(0).gameObject;
        flagContainer = gameObject.transform.GetChild(1).gameObject;
        decalContainter = gameObject.transform.GetChild(2).gameObject;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRendererContainer = containter.GetComponent<SpriteRenderer>();
        _spriteRendererFlagContainer = flagContainer.GetComponent<SpriteRenderer>();
        _spriteRendererDecalContainer = decalContainter.GetComponent<SpriteRenderer>();

        _spriteRendererMineFlagRenderer = flagContainer.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
        _spriteRendererDecalContainerRenderer = decalContainter.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        
        

    }

    // Update is called once per frame
    void Update()
    {
        _spriteRenderer.sprite = squareRevealed ? squareSpriteUsed : squareSpriteUnused;
        
        _spriteRendererContainer.sprite = hasNeighbourMine ? hasMine ? mine.sprite : NumberSprites.Instance.GetNumberedSprite(number) : null;

        _spriteRendererContainer.sortingOrder = squareRevealed ? 1 : -1;

        _spriteRendererDecalContainer.sprite = isLongNeighbour && squareRevealed ? DecalArrow : null;
        //_spriteRendererDecalContainerRenderer.sprite = isLongNeighbour && squareRevealed ? NumberSprites.Instance.GetNumberedSprite(longNumber) : null;
        
        _spriteRendererMineFlagRenderer.sprite = RunPlayerStats.Instance.FlagMineSelected == null ? null : RunPlayerStats.Instance.FlagMineSelected.sprite;
        
        if (hasMine && mine.isDisabled)
        {
            _spriteRendererContainer.sprite = mine.sprite;
            _spriteRendererContainer.color = Color.red;
            _spriteRendererContainer.sortingOrder = 1;
        }

        _spriteRendererFlagContainer.gameObject.SetActive(hasFlag);

        //RotateAround();
    }

    public void Interact()
    {
        if (hasFlag || (hasMine && mine.isDisabled) || RunPlayerStats.Instance.EndState || squareRevealed) return;
        
        if (RunPlayerStats.Instance.DebugMode)
        {
            squareRevealed = true;
            //if (!isBubbling) StartCoroutine(Bobble());
            SpawnBackground();
            StartCoroutine(OpenDoorAnimation(transform.parent.transform, transform.position, isOpen ? -1 : 1));
            //StartCoroutine(SwingCoroutine(isOpen ? -1 : 1));
            //StartCoroutine(Zoom(transform.position,transform.parent.transform));

            return;
        }
        
        MineRoomManager mineRoomManager = RunPlayerStats.Instance.MineRoomManager;

        if (!mineRoomManager.AfterFirstMove)
        {
            ActionEvents.Instance.TriggerEventAction();
            SoundManager.Instance.Play("Click", transform, true, 1, 1 + RunPlayerStats.Instance.Heat / 2);
            mineRoomManager.SetLogic(this);
            if (!isBubbling) StartCoroutine(Bobble());
            ActionEvents.Instance.TriggerEventAfterAction();
        }
        else
        {
            if (squareRevealed) return;
            
            ActionEvents.Instance.TriggerEventAction();
            RunPlayerStats.Instance.Points += (int)(RunPlayerStats.Instance.ComboValue*RunPlayerStats.Instance.PointsGain);
            RunPlayerStats.Instance.Heat += 0.15f;
            
            SoundManager.Instance.Play("Click", transform, true, 1, 1 + RunPlayerStats.Instance.Heat / 2);
            
            mineRoomManager.RevealTile(this);
            if (!isBubbling) StartCoroutine(Bobble());

            mineRoomManager.AfterActionFunction();
            ActionEvents.Instance.TriggerEventAfterAction();
        }
    }

    public void SecondInteract()
    {
        if (squareRevealed || RunPlayerStats.Instance.EndState) return;
        RunPlayerStats.Instance.currentEffectAbility.CallAbility(this);
    }

    public void HoverStart()
    {
        if(squareRevealed)return;
        transform.localScale *= 1.1f;
        _isHovered = true;
    }

    public void Hover()
    {
        //transform.rotation = quaternion.identity;
    }

    public void HoverEnd()
    {
        if(!_isHovered) return;
        transform.localScale /= 1.1f;
        _isHovered = false;
    }

    public void RotateAround()
    {
        // 1) Få mouse pos
        Vector3 mouseWorld = RunPlayerStats.Instance.Camera.ScreenToWorldPoint(Input.mousePosition);
        
        // 2) Räkna tilt mot musen (lokal 3D-känsla)
        Vector2 d = new Vector2(mouseWorld.x - transform.position.x, mouseWorld.y - transform.position.y);

        // “Naturlig” mättnad: arctan av avstånd/radius
        float tiltX = -Mathf.Atan2(d.y, radius) * Mathf.Rad2Deg; // pitch (X)
        float tiltY =  Mathf.Atan2(d.x, radius) * Mathf.Rad2Deg; // yaw   (Y)

        // Begränsa
        tiltX = Mathf.Clamp(tiltX, -maxAngle, maxAngle);
        tiltY = Mathf.Clamp(tiltY, -maxAngle, maxAngle);

        Quaternion targetRot = Quaternion.Euler(-tiltX, -tiltY, 0f);

        // 3) Smidig interpolation (frametålig)
        float t = 1f - Mathf.Exp(-smooth * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, t);

    }
    
    private IEnumerator Bobble()
    {
        isBubbling = true;

        Quaternion startRot = transform.rotation;
        float elapsed = 0f;

        while (elapsed < 2f) // bobble for ~2 seconds
        {
            elapsed += Time.deltaTime;

            // damped sine wave: big swing at first, slowly fades out
            float angle = Mathf.Sin(elapsed * bobbleSpeed) * bobbleAngle * Mathf.Exp(-elapsed * damping);

            // apply rotation only on Z
            transform.rotation = startRot * Quaternion.Euler(0f, 0f, angle);

            yield return null;
        }

        // reset to original rotation at end
        transform.rotation = startRot;

        isBubbling = false;
    }

    public string propertyName = "_CutoffHeight"; 
    public float startValue = 1.5f;
    public float endValue = -1.5f;
    public float duration = 1.0f;

    public void StartDissolve(float randomOrg)
    {
        StartCoroutine(DissolveRoutine(randomOrg));
    }

    private IEnumerator DissolveRoutine(float randomOrg)
    {
        // skapa en kopia av objektet

        GameObject ghost = new GameObject(gameObject.name + "_ShaderOBJ");
        
        ghost.transform.SetPositionAndRotation(transform.position, transform.rotation);
        ghost.transform.localScale = transform.localScale;
        ghost.transform.parent = transform;

        // ta renderern och ge den en unik instans av materialet
        SpriteRenderer rend = ghost.AddComponent<SpriteRenderer>();
        rend.sprite = _spriteRenderer.sprite;
        rend.sortingOrder = 4;
        Material mat = new Material(dissolveMaterial);
        rend.material = mat;

        // animera värdet
        float t = 0f;
        duration += randomOrg;
        while (t < duration)
        {
            float value = Mathf.Lerp(startValue, endValue, t / duration);
            mat.SetFloat(propertyName, value);
            t += Time.deltaTime;
            yield return null;
        }

        // sätt sista värdet
        mat.SetFloat(propertyName, endValue);

        // ta bort ghost när klart
        Destroy(ghost);
    }
    
    [Header("Setup")]
    [Tooltip("Sätt denna till ett empty GameObject placerat vid dörrens gångjärn (kanten).")]
    public Transform hingePivot;

    [Header("Motion")]
    [SerializeField] private float openAngle = 90f;   // hur mycket dörren öppnar
    [SerializeField] private float durationSwing = 1f;   // tid för öppna/stäng
    [SerializeField] private int direction = 1;       // 1 = öppna 'utåt', -1 = åt andra hållet

    private bool isOpen = false;
    private bool isAnimating = false;
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

    private float EaseInOutCubic(float x)
    {
        return x < 0.5f ? 4f*x*x*x : 1f - Mathf.Pow(-2f*x + 2f, 3f)/2f;
    }

    public void SpawnBackground()
    {
        GameObject gameObjectBackground = new GameObject();
        gameObjectBackground.transform.position = transform.position;
        SpriteRenderer spriteRenderer = gameObjectBackground.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = squareSpriteUnused;
        gameObjectBackground.transform.SetParent(transform.parent);
    }
    
    
    private float zoomStop = 20f;
    private float durationZoom = 1f;
    
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

    private IEnumerator OpenDoorAnimation(Transform startPos, Vector3 targetPos, int openSign)
    {
        //Move to position
        float elapsed = 0f;
        Vector3 startPosRef = startPos.position;
        while (elapsed < durationZoom)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / durationZoom);

            startPos.position = new Vector3(Mathf.Lerp(startPosRef.x, startPosRef.x - targetPos.x, t),
                Mathf.Lerp(startPosRef.y, startPosRef.y - targetPos.y, t),
                Mathf.Lerp(startPosRef.z, startPosRef.z - targetPos.z, t));

            yield return null;
        }

        //Door Swings
        if (hingePivot == null)
        {
            Debug.LogWarning("DoorHingeSwing: hingePivot saknas.");
            yield break;
        }

        isAnimating = true;

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
        
        
        
    }


}
