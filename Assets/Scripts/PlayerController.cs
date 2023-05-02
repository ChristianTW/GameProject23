using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Cant add player controller to game object unless a rigid body exist
//[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable)]

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 10f;
    public float jumpImpulse = 10f;
    //edit speeds in Unity, not VScode
    public float airSpeed = 7f;
    Vector2 moveInput;
    TouchingDirections touchingDirections;
    Damageable damageable;

    public float CurrentMoveSpeed { get
        {
            if(CanMove)
            {
            if(isMoving && !touchingDirections.IsOnWall)
            {
                if(touchingDirections.IsGrounded)
                {
                    return walkSpeed;
                } else {
                    //Air move speed
                    return airSpeed;
                }
            } else {
                //Idle speed is 0
                return 0;
            }
    }  else {
        //Movement locked
        return 0;
    } 
    } 
    }

    private bool _isMoving = false;

    public bool isMoving { get 
        {
            return _isMoving;
        }
        private set 
        {
                _isMoving = value;
                animator.SetBool("isMoving", value);
        }
    }

    public bool _isFacingRight = true;

    public bool IsFacingRight {get { return _isFacingRight; } private set{
        if(_isFacingRight != value)
        {
            // flip local scale
            transform.localScale *= new Vector2(-1,1);
        }
        _isFacingRight = value;
    } }

    public bool CanMove { get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public bool IsAlive {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    

    Rigidbody2D rb;
    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
    }

    private void FixedUpdate()
    {
        if(!damageable.LockVelocity)
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        
        moveInput = context.ReadValue<Vector2>();
        
        if(IsAlive)
        {
            isMoving = moveInput != Vector2.zero;

            SetFacingDirection(moveInput);
        } else {
            isMoving = false;
        }

    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight)
        {
            // Face right
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            // Face left
            IsFacingRight = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // TODO check if alive as well
        if(context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            animator.SetTrigger(AnimationStrings.attack);
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
}
