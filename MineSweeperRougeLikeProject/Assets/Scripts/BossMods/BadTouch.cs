using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[CreateAssetMenu(menuName = "BossMod/BadTouch", fileName = "BadTouch")]
public class BadTouch : BossModification
{
    public override string Description => "A 'Mouse-Seaking-Bomb' is created, explodes if it touches the mouse.";

    public Sprite BombSprite;

    public float speed = 6;

    public float turnSpeed = 1.5f;

    public float timeBank = 5;

    private GameObject gameObject;

    public override void Modification()
    {
        gameObject = new GameObject("Mouse Seaking Bomb");
        gameObject.transform.position = new Vector3(0,Camera.main.orthographicSize+3);
        SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = BombSprite;
        spriteRenderer.sortingOrder = 7;
        CircleCollider2D circleCollider2D = gameObject.AddComponent<CircleCollider2D>();
        MouseSeakingBomb mouseSeakingBomb = gameObject.AddComponent<MouseSeakingBomb>();
        mouseSeakingBomb.speed = speed;
        mouseSeakingBomb.turnSpeed = turnSpeed;
        mouseSeakingBomb.timeBank = timeBank;
    }

    public override void UnsubscribeModification()
    {
        base.UnsubscribeModification();
        Destroy(gameObject);
    }
}
