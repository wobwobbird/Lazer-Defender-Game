using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
   [SerializeField] float moveSpeed = 5f;
   [SerializeField] float paddingLeft;
   [SerializeField] float paddingRight;
   [SerializeField] float paddingTop;
   [SerializeField] float paddingBottom;
    Vector2 rawInput; 
    Vector2 minBounds; // saved value in worldspace of bottom left in worldspace
    Vector2 maxBounds; // saved value in worldspace of top right in worldspace
    Shooter shooter;

    void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    void Start()
    {
        InitBounds();
    }
 
    void Update()
    {
        Move();
    }

    void InitBounds()
    {
        Camera mainCamara = Camera.main;
        minBounds = mainCamara.ViewportToWorldPoint(new Vector2(0,0));
        maxBounds = mainCamara.ViewportToWorldPoint(new Vector2(1,1));
    }

    void Move()
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        transform.position = newPos;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnAttack(InputValue value)
    {
        if(shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }

}
