using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseSeakingBomb : MonoBehaviour, IInteractable
{
    public Vector2 target = new Vector2();
    public float speed = 6;
    public float accel = 0.2f;

    public Vector2 direction = Vector2.zero;
    public Vector2 currentDirection = Vector2.zero;

    public float turnSpeed = 1.5f;
    private float currentTurnSpeed = 0;

    public float timeBank;
    private float currentTime = 0;

    private Vector2 velocity => currentDirection * speed;

    // Start is called before the first frame update
    void Start()
    {
        target = new Vector2(0,0);
    }

    // Update is called once per frame
    void Update()
    { 
        currentTime += Time.deltaTime;
        if(currentTime < timeBank) return;

        target = Camera.main.ScreenToWorldPoint(RunPlayerStats.Instance.InputHandler.MousePosition);

        direction = (target-(Vector2)transform.position).normalized;

        currentTurnSpeed = Vector2.Distance((Vector2)transform.position,target) * turnSpeed;
        currentDirection = Vector2.Lerp(currentDirection, direction, currentTurnSpeed*Time.deltaTime).normalized;

        transform.position += (Vector3) velocity * Time.deltaTime;
        float rot_z = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
    }

    public void HoverStart()
    {
        //Explode
        RunPlayerStats.Instance.Health -= 1;
        SoundManager.Instance.Play("Explosion", transform, true, 1);
        transform.position = new Vector3(0,Camera.main.orthographicSize+3);
        currentTime = 0;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine((Vector2)transform.position,(Vector2)transform.position+(target-(Vector2)transform.position).normalized);

        Gizmos.color = Color.blue;

        Gizmos.DrawLine((Vector2)transform.position,(Vector2)transform.position+(target-(Vector2)transform.position).normalized*speed);    
    }
}
