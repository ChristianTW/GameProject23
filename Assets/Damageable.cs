using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    Animator animator;

    [SerializeField]
    private int _maxHealth = 100;
    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;

    public int MaxHealth
    {
        get
        {
            return _maxHealth = 100;
        }
        set
        {
            _maxHealth = value;
        }
    }

    private int _health = 100;

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;

            //If health drops below 0, character is no longer alive
            if(_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;

    [SerializeField]
    private bool isInvincible = false;

    public bool IsAlive { 
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
        }
    }

    private void awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if(isInvincible)
        {
            if(timeSinceHit > invincibilityTime)
            {
                // Remove invincibility
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }
    }

    public void Hit(int damage)
    {
        if(IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;
        }
    }
}
