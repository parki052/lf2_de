using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public float runSpeed = 4.0f;
    private float horizontal;
    private float vertical;

    private float yLowerBound = -3.5f;
    private float yUpperBound = 0.5f;
    private float xLeftBound = -10.5f;
    private float xRightBound = 10.5f;

    private float timeToUnblock_punch = 0.18f;
    private bool isPunching = false;
    private bool isRunningRight = false;
    private bool isRunningLeft = false;
    private float timer_start_punch;

    private float timeToUnblock_defend = 0.4f;
    private bool isDefending = false;
    private float timer_start_defend;


    private float timeToUnblockAfterStopRun = 0.25f;
    private float timer_start_run_stop;
    private bool movementBlockedFromRunStop = false;

    private bool movementBlocked = false;

    private string standAnim = "freeze_stand";
    private string walkAnim = "freeze_walk";
    private string runAnim = "freeze_run";
    private string runStopAnim = "freeze_run_stop";
    private string punchRightAnim = "freeze_punch_right";
    private string punchLeftAnim = "freeze_punch_left";
    private string defend = "freeze_defend";

    private bool facingRight = true;

    private float queuedMovePersistLength = 0.3f;
    private List<QueuedMove> moveQueue;

    private void Awake()
    {
        inputControls = new InputControls();
    }
    // Start is called before the first frame update
    void Start()
    {
        moveQueue = new List<QueuedMove>();
        playerAnim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        
        Debug.Log($"X: {body.position.x} Y: {body.position.y}");
        ProcessMoveQueue();

        Vector2 newVelocity = Vector2.negativeInfinity;

        if (Time.time - timer_start_punch > timeToUnblock_punch)
        {
            movementBlocked = false;
            isPunching = false;
        }
        if (Time.time - timer_start_defend > timeToUnblock_defend)
        {
            movementBlocked = false;
            isDefending = false;
        }
        if (Time.time - timer_start_run_stop > timeToUnblockAfterStopRun)
        {
            movementBlockedFromRunStop = false;
        }

        if (!movementBlocked && !isPunching && !isDefending && !movementBlockedFromRunStop)
        {
            moveInput = inputControls.Player.movement.ReadValue<Vector2>();
            horizontal = moveInput.x;
            vertical = moveInput.y;

            if (isRunningRight)
            {
                newVelocity = new Vector2(1 * runSpeed, (vertical * walkSpeed) * .7f);
            }
            else if (isRunningLeft)
            {
                newVelocity = new Vector2(-1 * runSpeed, (vertical * walkSpeed) * .7f);
            }

            if(!isRunningRight && !isRunningLeft && !movementBlockedFromRunStop)
            {
                if (horizontal == 0 && vertical == 0) //standing
                {
                    playerAnim.Play(standAnim);
                }
                if (horizontal != 0 && vertical != 0) //diagonal movement
                {

                }
                if (horizontal != 0 || vertical != 0) //moving
                {
                    if (!isRunningRight && !isRunningLeft)
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
                }
                newVelocity = new Vector2(horizontal * walkSpeed, vertical * walkSpeed);
            }
        }

        if(newVelocity != Vector2.negativeInfinity)
        {
            body.velocity = newVelocity;
        }

        body.position = new Vector2(Mathf.Clamp(body.position.x, xLeftBound, xRightBound), Mathf.Clamp(body.position.y, yLowerBound, yUpperBound));
    }
    private bool IsInRange(float toCheck, float min, float max)
    {
        if (toCheck >= min && toCheck <= max)
        {
            return true;
        }
        else return false;
    }

    private void ProcessMoveQueue()
    {
        moveQueue.RemoveAll(m => Time.time - m.InputTime > queuedMovePersistLength); //trim out inputs that happened too long ago

        if (moveQueue.Count > 0)
        {
            if (moveQueue.Last().Input == UserInput.Right)
            {
                var lastIndex = moveQueue.IndexOf(moveQueue.Last());
                if (moveQueue.Count > 1)
                {
                    var secondToLast = moveQueue[lastIndex - 1];

                    if (secondToLast.Input == UserInput.Right && !movementBlocked && !movementBlockedFromRunStop)
                    {
                        if (!facingRight)
                        {
                            facingRight = true;
                            Flip();
                        }
                        isRunningRight = true;
                        playerAnim.Play(runAnim);
                        moveQueue.RemoveRange(lastIndex - 1, 2);
                    }
                }
            }
            else if (moveQueue.Last().Input == UserInput.Left)
            {
                var lastIndex = moveQueue.IndexOf(moveQueue.Last());
                if (moveQueue.Count > 1)
                {
                    var secondToLast = moveQueue[lastIndex - 1];

                    if (secondToLast.Input == UserInput.Left && !movementBlocked && !movementBlockedFromRunStop)
                    {
                        if (facingRight)
                        {
                            facingRight = false;
                            Flip();
                        }
                        isRunningLeft = true;
                        playerAnim.Play(runAnim);
                        moveQueue.RemoveRange(lastIndex - 1, 2);
                    }
                }
            }
        }
    }

    private void HandleAttackStarted(InputAction.CallbackContext c)
    {
        var random = UnityEngine.Random.Range(0, 3);
        var moveXVector = facingRight ? 0.2f : -0.2f;
        if (!movementBlocked && !isPunching && !isDefending && !movementBlockedFromRunStop)
        {
            movementBlocked = true;
            isPunching = true;
            timer_start_punch = Time.time;
            body.velocity = new Vector2(moveXVector, 0);
            playerAnim.Play(random == 0 ? punchLeftAnim : punchRightAnim);
        }
    }

    private void OnEnable()
    {
        inputControls.Player.defend.started += c =>
        {
            HandleQueueMove(UserInput.Defend);
            HandleDefendStarted(c);
        };

        inputControls.Player.attack.started += c =>
        {
            HandleQueueMove(UserInput.Attack);
            HandleAttackStarted(c);
        };

        inputControls.Player.direction_up.started += c => HandleQueueMove(UserInput.Up);
        inputControls.Player.direction_down.started += c => HandleQueueMove(UserInput.Down);
        inputControls.Player.direction_left.started += c =>
        {
            
            if (isRunningRight)
            {
                StopMotion();
                isRunningRight = false;
                movementBlockedFromRunStop = true;
                playerAnim.Play(runStopAnim);
                body.velocity = new Vector2(3f, 0);
                timer_start_run_stop = Time.time;
            }
            else
            {
                HandleQueueMove(UserInput.Left);
            }
        };
        inputControls.Player.direction_right.started += c =>
        {
            if (isRunningLeft)
            {
                StopMotion();
                isRunningLeft = false;
                movementBlockedFromRunStop = true;
                playerAnim.Play(runStopAnim);
                body.velocity = new Vector2(-3f, 0);
                timer_start_run_stop = Time.time;
            }
            else
            {
                HandleQueueMove(UserInput.Right);
            }
        };

        inputControls.Player.Enable();
    }


    private void HandleQueueMove(UserInput input)
    {
        var move = new QueuedMove();
        move.Input = input;
        move.InputTime = Time.time;

        moveQueue.Add(move);
    }

    private void HandleDefendStarted(InputAction.CallbackContext c)
    {
        if (!movementBlocked && !isDefending && !movementBlockedFromRunStop)
        {
            movementBlocked = true;
            isDefending = true;
            timer_start_defend = Time.time;
            body.velocity = new Vector2(0, 0);
            playerAnim.Play(defend);
        }
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

    private void StopMotion() => body.velocity = new Vector2(0, 0);

}

class QueuedMove
{
    public UserInput Input { get; set; }
    public float InputTime { get; set; }
}

enum UserInput
{
    Up,
    Down,
    Left,
    Right,
    Attack,
    Jump,
    Defend
}