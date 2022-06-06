using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Camera cam;

    public Animator anim;

    Vector2 movement;
    Vector2 mousePos;

    private bool isRunning;
    private Vector3 moveDir;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (movement.x != 0 || movement.y != 0)
        {
            anim.SetFloat("vertical", movement.x);
            anim.SetFloat("vertical", movement.y);
            if (!isRunning)
            {
                isRunning = true;
                anim.SetBool("isRunning", isRunning);
            }
        }
        else
        {
            if (isRunning)
            {
                isRunning = false;
                anim.SetBool("isRunning", isRunning);
            }
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

        //rb.velocity = moveDir * moveSpeed * Time.deltaTime;
    }
}
