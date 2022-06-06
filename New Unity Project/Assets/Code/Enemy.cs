using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ITakeDamage
{
    public float moveSpeed = 5f;
    public float attackInterval;
    
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator anim;
    private GameObject playerGameObject;
    private Transform playerTransform;
    private int health = 3;
    private bool inRange;
    private Player damagable;
    private float nextAttack;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        playerTransform = playerGameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = playerTransform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
        
        anim.SetBool("isRunning", transform.hasChanged);

        if (inRange && Time.time > nextAttack)
        {
            damagable.TakeDamage(1);
            anim.SetBool("attack", true);
            nextAttack = Time.time + attackInterval;
        }
        else
        {
            anim.SetBool("attack", false);
        }
    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        damagable = other.GetComponent<Player>();
        if (damagable != null)
        {
            inRange = true;
        }
    }
}
