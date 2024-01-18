using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;


public class freeze_controller : MonoBehaviour
{
    //unity objects
    private Rigidbody2D rigidBody;
    private Animator animator;
    private InputControls inputControls;

    //animation file strings
    private string standAnim = "freeze_stand";
    private string walkAnim = "freeze_walk";
    private string runAnim = "freeze_run";
    private string runStopAnim = "freeze_run_stop";
    private string punchRightAnim = "freeze_punch_right";
    private string punchLeftAnim = "freeze_punch_left";
    private string defendAnim = "freeze_defend";
    private string rollAnim = "freeze_roll";
    private string runAttackAnim = "freeze_run_attack";

    //speed of movement during actions
    public const float walkSpeed = 2.3f;
    public const float runSpeed = 5.0f;
    public const float runStopSpeed = 3f;
    public const float punchSpeed = 0.17f;

    //speed of animations, and the calculated value of how long in ms an animation will take
    public const float runAttackSpeedMultiplier = 1f;
    public const float defendSpeedMultiplier = 0.2f;
    public const float punchSpeedMultiplier = 0.85f;
    public const float rollSpeedMultiplier = 0.7f;
    private int defendAnim_ms;
    private int punchAnim_ms;
    private int rollAnim_ms;
    private int runStopAnim_ms = 250; //don't need to calculate since it's a 1 frame animation
    private int runAttackAnim_ms;

    //character state
    private bool facingRight = true;
    private bool isDefending = false;
    private bool isAttacking = false;
    private bool isRunning = false;
    private bool runningRight = false;
    private bool isJumping = false;
    private bool movementBlocked = false;

    private List<QueuedInput> inputQueue;
    private float inputTimeoutAfter = 0.5f; //how long an input will stay in the queue before being removed

    private void Awake()
    {
        inputControls = new InputControls();
        inputQueue = new List<QueuedInput>();
    }

    private void OnEnable()
    {
        //register listeners for input events (single button, fire and forget, NOT continuously held buttons as with WASD movement
        //when an input event starts, add the input to the input queue
        inputControls.Player.attack.started += (c) => inputQueue.Add(new QueuedInput(Time.time, ActionInput.Attack, movementBlocked));
        inputControls.Player.defend.started += (c) => inputQueue.Add(new QueuedInput(Time.time, ActionInput.Defend, movementBlocked));
        inputControls.Player.jump.started += (c) => inputQueue.Add(new QueuedInput(Time.time, ActionInput.Jump, movementBlocked));
        inputControls.Player.direction_up.started += (c) => inputQueue.Add(new QueuedInput(Time.time, ActionInput.Up, movementBlocked));
        inputControls.Player.direction_down.started += (c) => inputQueue.Add(new QueuedInput(Time.time, ActionInput.Down, movementBlocked));
        inputControls.Player.direction_left.started += (c) => inputQueue.Add(new QueuedInput(Time.time, ActionInput.Left, movementBlocked));
        inputControls.Player.direction_right.started += (c) => inputQueue.Add(new QueuedInput(Time.time, ActionInput.Right, movementBlocked));

        inputControls.Player.Enable();
    }

    private void StopMovement()
    {
        animator.Play(standAnim);
        rigidBody.velocity = Vector2.zero;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();

        //set the animation's speed then save the animation's calculated duration
        animator.SetFloat("punchSpeed", punchSpeedMultiplier);
        animator.SetFloat("defendSpeed", defendSpeedMultiplier);
        animator.SetFloat("rollSpeed", rollSpeedMultiplier);
        animator.SetFloat("runAttackSpeed", runAttackSpeedMultiplier);

        punchAnim_ms = GetAnimationDurations_Ms(punchLeftAnim, punchSpeedMultiplier);
        defendAnim_ms = GetAnimationDurations_Ms(defendAnim, defendSpeedMultiplier);
        rollAnim_ms = GetAnimationDurations_Ms(rollAnim, rollSpeedMultiplier);
        runAttackAnim_ms = GetAnimationDurations_Ms(runAttackAnim, runAttackSpeedMultiplier);
    }

    void Update()
    {

    }

    private async void FixedUpdate()
    {
        //the input queue is handled, and if needed, actions/animations/movement is triggered
        await HandleInputQueue();

        //after handling the input queue, standing/running/walking animations and movement are handled
        HandlePlayerMovement();
    }
    private async Task HandleInputQueue()
    {
        //trim out inputs that happened too long ago
        foreach (var input in inputQueue.ToList())
        {
            if (Time.time - input.InputTime > inputTimeoutAfter)
            {
                inputQueue.Remove(input);
            }
        }

        /*     INPUT QUEUE PROCESSING SECTION
            1. try to detect special moves
            2. try to detect running
            3. try to detect chained attack / defend / jump
            4. if no input combo is found, process press as a singular input
            
            ~ only try a next step if we haven't triggered an action yet
            ~ clear entire queue if special move / running is triggered
        */

        bool foundAction = false;

        // 1. special moves section
        if (inputQueue.Count >= 3)
        {
            var lastThreeInputs = inputQueue.TakeLast(3);
            var inputList = "";
            foreach (var input in lastThreeInputs)
            {
                inputList += input.Input.ToString() + " ";
            }
            Debug.Log("Hit special moves. Current moves: " + inputList);
        }
        // 2. detect running section
        if (inputQueue.Count >= 2 && !foundAction)
        {
            var lastTwoInputs = inputQueue.TakeLast(2);

            if (lastTwoInputs.All(input => input.Input == ActionInput.Left) || lastTwoInputs.All(input => input.Input == ActionInput.Right))
            {
                if (!movementBlocked)
                {
                    runningRight = lastTwoInputs.Last().Input == ActionInput.Right;

                    isRunning = true;
                    inputQueue.Clear();
                    foundAction = true;
                }
            }
        }
        // 3. detect chained attack / defend / jump section
        if (inputQueue.Count >= 2 && !foundAction)
        {

        }
        // 4. process singular inputs
        if (inputQueue.Count >= 1 && !foundAction)
        {
            var lastInput = inputQueue.Last().Input;
            //handle singular input while running
            if (isRunning)
            {
                //handle stopping running
                if (lastInput == ActionInput.Left && runningRight || lastInput == ActionInput.Right && !runningRight)
                {
                    bool goingRight = runningRight;

                    StopMovement();
                    isRunning = false;
                    movementBlocked = true;
                    animator.Play(runStopAnim);
                    rigidBody.velocity = new Vector2(goingRight ? runStopSpeed : -runStopSpeed, 0);
                    await Task.Delay(runStopAnim_ms);
                    StopMovement();
                    movementBlocked = false;
                    inputQueue.Clear();
                }
                //handle rolling
                else if (lastInput == ActionInput.Defend)
                {
                    StopMovement();
                    isRunning = false;
                    movementBlocked = true;
                    animator.Play(rollAnim);
                    rigidBody.velocity = new Vector2(runningRight ? runSpeed : -runSpeed, 0);
                    await Task.Delay(rollAnim_ms);
                    StopMovement();
                    movementBlocked = false;
                    inputQueue.Clear();
                }
                //handle running attack
                else if (lastInput == ActionInput.Attack)
                {
                    StopMovement();
                    isRunning = false;
                    movementBlocked = true;
                    animator.Play(runAttackAnim);
                    rigidBody.velocity = new Vector2(runningRight ? runSpeed : -runSpeed, 0);
                    await Task.Delay((int)(runAttackAnim_ms * .45));
                    rigidBody.velocity = new Vector2(0, 0);
                    await Task.Delay((int)(runAttackAnim_ms * .55));
                    movementBlocked = false;
                    inputQueue.Clear();
                }
                //handle running jump
                else if (lastInput == ActionInput.Jump)
                {

                }
            }
            //handle singular input while not running and input is not blocked
            else if (!movementBlocked)
            {
                //handle singular attack
                if (lastInput == ActionInput.Attack)
                {
                    movementBlocked = true;

                    var anim = RNG(38) ? punchLeftAnim : punchRightAnim;
                    animator.Play(anim);
                    rigidBody.velocity = new Vector2(facingRight ? punchSpeed : -punchSpeed, 0);

                    await Task.Delay(punchAnim_ms);
                    movementBlocked = false;
                    inputQueue.Clear();
                }
                //handle singular jump
                else if (lastInput == ActionInput.Jump)
                {

                }
                //handle singular defend
                else if (lastInput == ActionInput.Defend)
                {
                    movementBlocked = true;
                    rigidBody.velocity = new Vector2(0, 0);
                    animator.Play(defendAnim);

                    await Task.Delay(defendAnim_ms);
                    animator.Play(standAnim);
                    await Task.Delay(50);
                    movementBlocked = false;
                    inputQueue.Clear();
                }

            }
        }
    }

    private void Flip()
    {
        Vector2 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    /// <summary>
    /// Returns a bool calculated by the given percent chance (an int between 1-100 inclusive) of being true 
    /// </summary>
    /// <param name="percentChance"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private bool RNG(int percentChance)
    {
        if (percentChance < 1 || percentChance > 100)
        {
            throw new Exception("GetRandom() requires percent chance to be between 1 and 100.");
        }
        return UnityEngine.Random.Range(1, 101) < percentChance;
    }

    private void HandlePlayerMovement()
    {
        if (!movementBlocked)
        {
            var vectorInput = inputControls.Player.movement.ReadValue<Vector2>();
            var x = vectorInput.x;
            var y = vectorInput.y;

            var newVelocity = new Vector2(0, 0);

            if (isRunning) //are we running around
            {
                if ((runningRight && !facingRight) || (!runningRight && facingRight))
                {
                    Flip();
                }
                animator.Play(runAnim);
                newVelocity = new Vector2(facingRight ? runSpeed : -runSpeed, (y * walkSpeed) * .7f);

            }
            else if ((x > 0 || y > 0) || (x < 0 || y < 0)) //are we walking around
            {
                if ((facingRight && x < 0) || (!facingRight && x > 0))
                {
                    Flip();
                }
                animator.Play(walkAnim);
                newVelocity = new Vector2(x * walkSpeed, y * walkSpeed);
            }
            else if (x == 0 && y == 0) //are we standing still
            {
                animator.Play(standAnim);
            }

            rigidBody.velocity = newVelocity;
        }
    }

    /// <summary>
    /// Take a given animator clip name and a speed multiplier percentage, and return how long in ms how long the clip is
    /// </summary>
    /// <param name="clipName"></param>
    /// <param name="speedMultiplier"></param>
    /// <returns></returns>
    private int GetAnimationDurations_Ms(string clipName, float speedMultiplier)
    {
        var clip = animator.runtimeAnimatorController.animationClips.Single(clip => clip.name == clipName);
        var durationMs = (int)((clip.length / speedMultiplier) * 1000);
        Debug.Log(clip.name + " - " + clip.length);
        return durationMs;
    }
}
