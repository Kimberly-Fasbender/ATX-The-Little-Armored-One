using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;

    Rigidbody2D rigidBody;
    Animator animator;
    Collider2D collider;
    CapsuleCollider2D capsuleCollider;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        MirrorSprite();
        Jump();
    }

    private void Run()
    {
        float moveInput = Input.GetAxis("Horizontal"); 
        Vector2 playerVelocity = new Vector2(moveInput * runSpeed, rigidBody.velocity.y);
        rigidBody.velocity = playerVelocity;

        // animation transition 
        bool isRunning = Mathf.Abs(rigidBody.velocity.x) > 0;
        animator.SetBool("Running", isRunning);
    }

    private void MirrorSprite()
    {
        bool isRunning = Mathf.Abs(rigidBody.velocity.x) > 0;

        if (isRunning) 
        {
            transform.localScale = new Vector2 (Mathf.Sign(rigidBody.velocity.x), 1f);
        }

    }

    private void Jump()
    {
        LayerMask ground = LayerMask.GetMask("Ground");
        bool isTouchingGround = collider.IsTouchingLayers(ground);

        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            Vector2 velocityToAdd = new Vector2(0f, jumpSpeed);
            rigidBody.velocity += velocityToAdd;

            animator.SetTrigger("TakingOff");

        }

        // animation transition
        // bool hasVerticalVelocity = Mathf.Abs(myRigidBody.velocity.y) > 0;
        animator.SetBool("Jumping", !isTouchingGround); 
        // Vector2 unrolledColliderSize = new Vector2(capsuleCollider.size.x, capsuleCollider.size.y);


        if (animator.GetBool("Jumping") == true)
        {
            Debug.Log("Jumping is True");
            capsuleCollider.offset = new Vector2(-0.005f, 0.005f);
            capsuleCollider.size = new Vector2(0.0001f, 0.45f);
        } 
        else 
        {
            Debug.Log("Jumping is False");
            capsuleCollider.offset = new Vector2(-0.01262277f, 0.0188039f);
            capsuleCollider.size = new Vector2(0.9990718f, 0.6624015f);
        }
    }
}
