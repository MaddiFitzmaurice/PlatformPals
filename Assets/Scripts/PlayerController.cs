using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Component Declaration
    [SerializeField]
    private Rigidbody rb;

    //Input Declaration
    [SerializeField]
    private string movementInput;
    private float horizontalMovement;
    [SerializeField]
    private string jumpInput;
    private float verticalMovement;

    //Object State Declaration
    public bool isGrounded;
    public bool isFalling;
    public bool canJump;
    public bool attemptJump;
    public bool isMovingRight = true;
    public bool isFacingRight = true;

    //Check Declaration
    [SerializeField]
    private Transform feet;
    private float timeOffGround;

    //Layer Declaration
    [SerializeField]
    private LayerMask groundLayer;

    //animator Declaration
    public Animator anim;

    //Player Stat Declaration
    [SerializeField]
    private float coyoteTime;
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float fallSpeed;
    [SerializeField]
    private float acceleration;
    [SerializeField]
    private float deceleration;
    [SerializeField]
    private float airAcceleration;
    [SerializeField]
    private float airDeceleration;
    [SerializeField] 
    private float velocityPower;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float gravity;
    [SerializeField]
    private float gravityScale;
    [SerializeField]
    private float terminalVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        horizontalMovement = Input.GetAxisRaw(movementInput);
        verticalMovement = Input.GetAxis(jumpInput);

        if (verticalMovement > 0)
        {
            attemptJump = true;
        }
        else
        {
            attemptJump = false;
        }

        if (Physics.Raycast(feet.position, -Vector2.up, 0.1f, groundLayer))
        {
            isGrounded = true;
            canJump = true;
        }
        else
        {
            isGrounded = false;
        }

        if(!isGrounded)
        {
            timeOffGround += Time.deltaTime;
        }
        else
        {
            timeOffGround = 0;
        }

        if (timeOffGround >= coyoteTime)
        {
            canJump = false;
        }

        if (horizontalMovement > 0.5f)
        {
            isMovingRight= false;
        }
        else if (horizontalMovement < -0.5f)
        {
            isMovingRight = true;
        }

        if (isMovingRight != isFacingRight)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            isFacingRight = !isFacingRight;
        }

        Debug.Log(horizontalMovement);
    }

    private void FixedUpdate()
    {
        Move();

        Gravity();

        if ((canJump) && (attemptJump))
        {
            Jump();
        }
    }

    public void Move()
    {
        float targetVelocity = walkSpeed* horizontalMovement;

        float velocityDifference = targetVelocity - rb.velocity.x;

        float accelerationRate = (Mathf.Abs(targetVelocity) > 0.1f) ? acceleration : deceleration;

        if (!isGrounded)
        {
            accelerationRate = (Mathf.Abs(targetVelocity) > 0.1f) ? airAcceleration : airDeceleration;
        }

        float movement = Mathf.Pow(Mathf.Abs(velocityDifference) * accelerationRate, velocityPower) * Mathf.Sign(velocityDifference);

        rb.AddForce(movement * Vector3.right);
    }

    public void Jump()
    {
        rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
        canJump = false;
        timeOffGround = coyoteTime;
    }

    public void Gravity()
    {
        if (rb.velocity.y > -terminalVelocity)
        {
            Vector3 downForce = gravity * gravityScale * Vector3.down;
            rb.AddForce(downForce, ForceMode.Acceleration);
        }
    }
}
