using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[CreateAssetMenu(menuName = "BossMod/LightsOut", fileName = "LightsOut")]
public class LightsOut : BossModification
{
    public override string Description => "You have limited vision around your Mouse";

    public Material material;
    public Sprite shadowSprite;

    private GameObject gameObject;

    private SpriteRenderer spriteRenderer;

    public override void Modification()
    {
        gameObject = new GameObject("Shadow Cover");
        gameObject.transform.localScale = Vector2.one * 100f;
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = shadowSprite;
        spriteRenderer.material = Instantiate(material);
        spriteRenderer.sortingOrder = 10;
        material.SetVector("_MousePosition", Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
    }

    public override void UpdateModification()
    {
        base.UpdateModification();
        spriteRenderer.material.SetVector("_MousePosition", Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
    }

    public override void UnsubscribeModification()
    {
        base.UnsubscribeModification();
        Destroy(gameObject);
    }
}
