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
    private Vector2 mouseWorldPosition;

    private Interactable currentCollidingInteractable;
    
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        
        myRigidbody2D = GetComponent<Rigidbody2D>();
        
       
    }


    void Update()
    {
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Run();
        FlipSprite();
    }

   


    private void OnMove(InputValue value)
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
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (mouseWorldPosition.x < myRigidbody2D.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    #region Interactions with Interactable

    private void OnInteract()
    {
        if (currentCollidingInteractable == null) return;
        currentCollidingInteractable.StartInteract();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        Interactable interactable = col.gameObject.GetComponent<Interactable>();
        if(interactable != null)
        {
            currentCollidingInteractable = interactable;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (currentCollidingInteractable == null) return;
        
        currentCollidingInteractable.StopInteract();
        currentCollidingInteractable = null; /* only interactable class should set this back to null */
    }

    #endregion
    
}
