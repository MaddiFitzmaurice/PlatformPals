using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask jumpableGround;
    private Rigidbody rb;
    [SerializeField] public float speed;
    [SerializeField] public float maxSpeed;
    [SerializeField] public float acceleration;

    [SerializeField] public float minJump;
    [SerializeField] public float maxJump;

    private SpriteRenderer spriteRenderer;

    private float inputAxis;
    public bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HorizontalMovement();

        if (isGrounded)
        {
            GroundedMovement();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contactPoint = collision.contacts[0];
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    private void HorizontalMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if ((speed < maxSpeed) && (speed > -maxSpeed) && (horizontalInput != 0))
        {
            speed = speed + horizontalInput * acceleration * Time.deltaTime;
        }
        else if (horizontalInput == 0)
        {
            if (speed > 0)
            {
                speed = speed - acceleration * Time.deltaTime;
            }
            if (speed < 0)
            {
                speed = speed + acceleration * Time.deltaTime;
            }
        }

            rb.velocity = new Vector3(speed, rb.velocity.y, 0);
    }

    private void GroundedMovement()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, maxJump, 0f);
    }
}
