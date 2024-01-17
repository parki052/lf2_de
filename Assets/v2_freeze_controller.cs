using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class v2_freeze_controller : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Animator animator;
    private InputControls inputControls;


    private string standAnim = "freeze_stand";
    private string walkAnim = "freeze_walk";
    private string runAnim = "freeze_run";
    private string runStopAnim = "freeze_run_stop";
    private string punchRightAnim = "freeze_punch_right";
    private string punchLeftAnim = "freeze_punch_left";
    private string defend = "freeze_defend";

    public float walkSpeed = 3.0f;
    public float runSpeed = 4.0f;

    private bool facingRight = true;

    private void Awake()
    {
        inputControls = GetComponent<InputControls>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Debug.Log("good");
        var directionalInput = inputControls.Player.movement.ReadValue<Vector2>();

        animator.Play(walkAnim);
        rigidBody.velocity = new Vector2(directionalInput.x * walkSpeed, directionalInput.y * walkSpeed);
    }

    private void Flip(bool flipX, bool flipY)
    {
        Vector2 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

}
