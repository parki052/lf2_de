using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayer : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator playerAnim;
    public float walkSpeed = 3.0f;
    public float moveLimiter = 0.7f;
    private float horizontal;
    private float vertical;

    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (horizontal == 0 && vertical == 0)
        {
            playerAnim.Play("bandit_stand");
        }
        if (horizontal != 0 && vertical != 0) //slow diagonal movement
        {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }
        if(horizontal != 0 || vertical != 0 ) //moving
        {
            playerAnim.Play("bandit_walk");
            if(horizontal < 0 && facingRight)
            {
                Flip();
            }
            if(horizontal > 0 && !facingRight)
            {
                Flip();
            }
        }

        body.velocity = new Vector2(horizontal * walkSpeed, vertical * walkSpeed);
    }

    private void Flip()
    {
        Vector2 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
}
