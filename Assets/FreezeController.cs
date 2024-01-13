using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FreezeController : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator playerAnim;
    private InputControls inputControls;
    private Vector2 moveInput;
    public float walkSpeed = 3.0f;
    private float horizontal;
    private float vertical;

    private float timeToUnblock = 0.18f;
    private float timer_total;
    private float timer_start;

    private bool movementBlocked = false;
    private bool isPunching = false;
    private bool attackDown = false;

    private string standAnim = "freeze_stand";
    private string walkAnim = "freeze_walk";
    private string runAnim = "freeze_run";
    private string punchRightAnim = "freeze_punch_right";
    private string punchLeftAnim = "freeze_punch_left";

    private bool facingRight = true;

    private void Awake()
    {
        inputControls = new InputControls();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        
        if(Time.time - timer_start > timeToUnblock)
        {
            movementBlocked = false;
            isPunching = false;
        }

        if (!movementBlocked && !isPunching)
        {
            moveInput = inputControls.Player.Movement.ReadValue<Vector2>();
            horizontal = moveInput.x;
            vertical = moveInput.y;


            if (horizontal == 0 && vertical == 0) //standing
            {
                playerAnim.Play(standAnim);
            }
            if (horizontal != 0 && vertical != 0) //diagonal movement
            {

            }
            if (horizontal != 0 || vertical != 0) //moving
            {
                playerAnim.Play(walkAnim);
                if (horizontal < 0 && facingRight)
                {
                    Flip();
                }
                if (horizontal > 0 && !facingRight)
                {
                    Flip();
                }
            }

            body.velocity = new Vector2(horizontal * walkSpeed, vertical * walkSpeed);
        }
    }

    private void HandleAttackStarted(InputAction.CallbackContext c)
    {
        var random = UnityEngine.Random.Range(0, 3);
        var moveXVector = facingRight ? 0.2f : -0.2f;
        if (!movementBlocked && !isPunching)
        {
            movementBlocked = true;
            isPunching = true;
            timer_start = Time.time;
            body.velocity = new Vector2(moveXVector, 0);
            playerAnim.Play(random == 0 ? punchLeftAnim : punchRightAnim);   
        }
        if (isPunching)
        {
            var timer = new Timer();
        }
    }

    private void OnEnable()
    {
        inputControls.Player.Attack.started += c => HandleAttackStarted(c);
        inputControls.Player.Attack.performed += c => HandleAttackFinished(c);
        inputControls.Player.Enable();     
    }

    private void HandleAttackFinished(InputAction.CallbackContext c)
    {
        //movementBlocked = false;
    }

    private void OnDisable()
    {
        inputControls.Player.Disable();
    }


    private void Flip()
    {
        Vector2 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

}

class MoveQueue
{
    private float timer;
}
