using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[CreateAssetMenu(menuName = "BossMod/BadTouch", fileName = "BadTouch")]
public class BadTouch : BossModification
{
    public override string Description => "A 'Mouse-Seaking-Bomb' is created, explodes if it touches the mouse.";

    public Sprite BombSprite;

    public override void Modification()
    {
        GameObject gameObject = new GameObject("Mouse Seaking Bomb");
        gameObject.transform.position = new Vector3(0,Camera.main.orthographicSize);
        SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = BombSprite;
        Rigidbody2D rigidbody = gameObject.AddComponent<Rigidbody2D>();
        MouseSeakingBomb mouseSeakingBomb = gameObject.AddComponent<MouseSeakingBomb>();
    }
}
