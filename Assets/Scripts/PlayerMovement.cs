using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;
    
    
    
    private Vector2 moveInput;
    private Animator myAnimator;
  
    private Rigidbody2D myRigidbody2D;
    private SpriteRenderer mySpriteRenderer;
    private Vector2 mouseWorldPosition;

    //
    private Vector2 weaponRotateDirection;

    private Vector3 playerCurrentVelocity;
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        
        myRigidbody2D = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        playerCurrentVelocity = myRigidbody2D.velocity;
    }

    void Update()
    {
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Run();
        FlipSprite();
    }

   


    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
   

   

   

    

    private void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x, moveInput.y) * speed;
        myRigidbody2D.velocity = playerVelocity;
    
        
        // For starting up the move animation
        bool playerIsMoving = Mathf.Abs(moveInput.x) + Mathf.Abs(moveInput.y) > Mathf.Epsilon;
        myAnimator.SetBool("isMoving", playerIsMoving);
            
    }

    private void FlipSprite()
    {
        if (mouseWorldPosition.x > myRigidbody2D.position.x)
        {
            transform.localScale = new Vector3(5, 5, 5);
        }
        else if (mouseWorldPosition.x < myRigidbody2D.position.x)
        {
            transform.localScale = new Vector3(-5, 5, 5);
        }
    }

    
}
