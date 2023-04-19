using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Component Declaration
    [SerializeField] private Rigidbody rb;

    //Input Declaration
    private float horizontalInput;

    //Object State Declaration
    public bool canJump;
    public bool attemptJump;
    public bool grounded;
    public bool isFacingRight;

    //Check Declaration
    [SerializeField] private Transform feet;
    private float timeOffGround;
    [SerializeField] private float coyoteTime;

    //Layer Declaration
    [SerializeField] private LayerMask groundLayer;

    //Player Stat Declaration
    [SerializeField] private float groundSpeed;
    [SerializeField] private float airSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;
    [SerializeField] private float jumpForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            attemptJump = true;
            
        }
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            attemptJump = false;
        }

        if (Physics.Raycast(feet.position, -Vector2.up, 0.1f, groundLayer))
        {
            grounded = true;
            canJump = true;
        }
        else
        {
            grounded = false;
        }
    }

    private void FixedUpdate()
    {
        Move();

        if ((attemptJump) && (canJump))
        {
            Jump();
        }

        if ((!attemptJump) && (!grounded))
        {
            //CutJump();
        }
    }

    public void Jump()
    {
        rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
        canJump = false;
        timeOffGround = 0f;
    }

    public void Move()
    {
        float targetVelocity;
        if (grounded)
        {
            targetVelocity = groundSpeed * horizontalInput;
        }
        else
        {
            targetVelocity = airSpeed * horizontalInput;
        }

        float velocityDifference = targetVelocity - rb.velocity.x;

        float accelerationRate = (Mathf.Abs(targetVelocity) > 0.01f) ? acceleration : deceleration;

        float movement = Mathf.Abs(velocityDifference) * acceleration * Mathf.Sign(velocityDifference);

        rb.AddForce(movement * Vector3.right);
    }
}
