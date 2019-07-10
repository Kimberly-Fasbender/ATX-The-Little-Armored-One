using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;

    Rigidbody2D myRigidBody;
    Animator myAnimator;


    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
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
        float controlThrow = Input.GetAxis("Horizontal"); 
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        // animation transition 
        bool hasHorizontalVelocity = Mathf.Abs(myRigidBody.velocity.x) > 0;
        myAnimator.SetBool("Running", hasHorizontalVelocity);
    }

    private void MirrorSprite()
    {
        bool hasHorizontalVelocity = Mathf.Abs(myRigidBody.velocity.x) > 0;

        if (hasHorizontalVelocity) 
        {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidBody.velocity.x), 1f);
        }

    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Vector2 velocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += velocityToAdd;
        }

        // // animation transition
        // bool hasVerticalVelocity = Mathf.Abs(myRigidBody.velocity.y) > 0;
        // myAnimator.SetBool("Jump", hasVerticalVelocity); 
    }
}
