using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseSeakingBomb : MonoBehaviour
{
    public Vector2 target = new Vector2();
    public float speed = 2;
    private float currentSpeed;
    public float accel = 2;

    public Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        target = new Vector2(0,0);
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    { 
        //TODO: Make it so that it overshoots its target, make velocity and make it move towards that instead.
        target = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        currentSpeed += Mathf.Min(accel * Time.deltaTime, 1);    // limit to 1 for "full speed"
        Vector3 pos = Vector3.MoveTowards(transform.position, target, speed * currentSpeed * Time.deltaTime);

        rigidbody.MovePosition(pos);
    }
}
