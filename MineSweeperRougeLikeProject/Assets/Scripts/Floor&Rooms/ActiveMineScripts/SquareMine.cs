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
    void OnEnable()
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
        RotateAround();
    }

    #region Setters
    public void SetUpSquareVisual()
    {
        SetContainerSprite();
        SetFlagSprite();
        SetRevealed(squareRevealed);
    }

    public void SetContainerSprite(Sprite sprite = null)
    {
        _spriteRendererDecalContainer.sprite = isLongNeighbour ? DecalArrow : null;
        // If a sprite is provided, use it. If not, check if the square has a mine. Else, use the mine's sprite. Otherwise, check if it has a neighbouring mine. If it does, use the numbered sprite corresponding to the number of neighbouring mines. If none of these conditions are met, set the sprite to null.
        _spriteRendererContainer.sprite = sprite != null ? sprite :
            hasMine ? mine.sprite :
            hasNeighbourMine ? NumberSprites.Instance.GetNumberedSprite(number) : null;
    }

    public void SetFlagSprite()
    {
        Sprite sprite = RunPlayerStats.Instance.FlagMineSelected?.sprite is not null ? RunPlayerStats.Instance.FlagMineSelected.sprite : null;  

        _spriteRendererMineFlagRenderer.sprite = sprite;

        _spriteRendererFlagContainer.gameObject.SetActive(hasFlag);
    }

    public void SetRevealed(bool squareRevealed)
    {
        this.squareRevealed = squareRevealed;

        _spriteRenderer.sprite = squareRevealed ? squareSpriteUsed : squareSpriteUnused;
        _spriteRendererContainer.sortingOrder = squareRevealed ? 1 : -1;
        _spriteRendererDecalContainer.gameObject.SetActive(squareRevealed);
    }

    public void SetDisabled(bool disabled)
    {
        if (hasMine)
        {
            mine.isDisabled = disabled;
            _spriteRendererContainer.sprite = mine.sprite;
            _spriteRendererContainer.color = disabled ? Color.red : Color.white;
            _spriteRendererContainer.sortingOrder = disabled ? 1 : -1;
        }
    }

    public void RevealContainer()
    {
        SetContainerSprite();
        _spriteRendererContainer.sortingOrder = 1;
    
    }
    #endregion

    public void Interact()
    {
        //Has the player reached the end state, or is the square already revealed, or has a flag been placed on it, or is the mine disabled? If any of these conditions are true, return early and do nothing.
        if (hasFlag || (mine is not null && mine.isDisabled) || RunPlayerStats.Instance.EndState || squareRevealed) return;
        
        //Debug mode: If the player is in debug mode, reveal the square and start the bobble animation if it isn't already playing, then return early.
        if (RunPlayerStats.Instance.DebugMode)
        {
            squareRevealed = true;
            if (!isBubbling) StartCoroutine(Bobble());

            return;
        }
        
        //Check if it is the first move of the game.
        MineRoomManager mineRoomManager = RunPlayerStats.Instance.MineRoomManager;

        if (!mineRoomManager.AfterFirstMove)
        {
            ActionEvents.Instance.TriggerEventAction();
            SoundManager.Instance.Play("Click", transform, true, 1, 1 + RunPlayerStats.Instance.Heat / 2);
            mineRoomManager.SetLogic(this);
            if (!isBubbling) StartCoroutine(Bobble());
        }
        else
        {
            if (squareRevealed) return;
            
            ActionEvents.Instance.TriggerEventAction();
            RunPlayerStats.Instance.Points += (int)(RunPlayerStats.Instance.ComboValue*RunPlayerStats.Instance.PointsGain);
            RunPlayerStats.Instance.Heat += RunPlayerStats.Instance.HeatGain;
            
            SoundManager.Instance.Play("Click", transform, true, 1, 1 + RunPlayerStats.Instance.Heat / 2);
            
            mineRoomManager.RevealTile(this);
            if (!isBubbling) StartCoroutine(Bobble());
        }

        ActionEvents.Instance.TriggerEventAfterAction();

        // we want to make sure this action is called after the action events have been triggered, so we call it at the end of the Interact method.
        mineRoomManager.AfterActionFunction();
    }

    public void SecondInteract()
    {
        if (squareRevealed || RunPlayerStats.Instance.EndState) return;
        RunPlayerStats.Instance.currentEffectAbility.CallAbility(this);
    }

    public void HoverStart()
    {
        if(squareRevealed || (hasMine && mine.isDisabled))return;
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
        try
        {
            if(gameObject.IsDestroyed()) return;
            transform.localScale /= 1.1f;
        }
        catch
        {
            Debug.LogWarning("SquareMine: HoverEnd() failed because the gameObject was destroyed.");
            _isHovered = false;
        }
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
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, t);

    }
    
    private IEnumerator Bobble()
    {
        isBubbling = true;

        Quaternion startRot = transform.localRotation;
        float elapsed = 0f;

        while (elapsed < 2f) // bobble for ~2 seconds
        {
            elapsed += Time.deltaTime;

            // damped sine wave: big swing at first, slowly fades out
            float angle = Mathf.Sin(elapsed * bobbleSpeed) * bobbleAngle * Mathf.Exp(-elapsed * damping);

            // apply rotation only on Z
            transform.localRotation = startRot * Quaternion.Euler(0f, 0f, angle);

            yield return null;
        }

        // reset to original rotation at end
        transform.localRotation = startRot;

        isBubbling = false;
    }

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
        rend.sprite = squareSpriteUnused;
        rend.sortingOrder = 4;
        Material mat = new Material(dissolveMaterial);
        rend.material = mat;

        // animera värdet
        float t = 0f;
        mat.SetFloat("_Noise_Strength", UnityEngine.Random.Range(0.8f, 1.2f));
        duration += randomOrg;
        while (t < duration)
        {
            float value = Mathf.Lerp(startValue, endValue, t / duration);
            mat.SetFloat("_CutoffHeight", value);
            t += Time.deltaTime;
            yield return null;
        }

        // sätt sista värdet
        mat.SetFloat("_CutoffHeight", endValue);

        // ta bort ghost när klart
        Destroy(ghost);
    }
}
