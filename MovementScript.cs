using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{

    private Rigidbody2D myRigidbody;

    private Animator myAnimator;

    [SerializeField]
    private float movementSpeed;

    //--For flipping character sprite\\
    private bool facingRight;

    //--For jumping\\
    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;

    private bool isGrounded;

    private bool jump;

    [SerializeField]
    private float jumpForce;

    //--End for jumping\\

    void Start()
    {
        facingRight = false;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
       
    }

    //-- Declaring most things \\
    void FixedUpdate()
    {

        float horizontal = Input.GetAxis("Horizontal");

        HandleMovement(horizontal);

        isGrounded = IsGrounded();

        Flip(horizontal);

        ResetValues();
    }

    void Update()
    {
        HandleInput();
    }

    //-- Any key downs \\
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            jump = true;
            Debug.Log("jumping active"); //TEMP FOR TESTING\\
        }

    }

    //-- Movement for the character and setting up animations \\
    private void HandleMovement(float horizontal)
    {
        myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));

        if (isGrounded && jump)
        {
            isGrounded = false;
            myRigidbody.AddForce(new Vector2(0, jumpForce));
        }
    }

    //-- Flipping the character \\
    private void Flip(float horizontal) 
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;

        }
    }

    //-- Resetting values each time when played\\
    private void ResetValues()
    {
        jump = false;
    }

    //-- Jumping \\
    private bool IsGrounded()
    {
        if (myRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

} //--End of program\\
