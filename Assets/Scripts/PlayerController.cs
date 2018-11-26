using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private const int MOVEMENT_SPEED = 500;
    private const int JUMP_SPEED = 500;
    private Animator playerAnimator;
    private SpriteRenderer playerRenderer;
    private bool IsOnTheGround;

	// Use this for initialization
	void Start ()
    {

        rb2d = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerRenderer = GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update ()
    {
		
        if (rb2d != null)
        {
            float xMovement = Input.GetAxis("Horizontal");

            if (xMovement > .2f || xMovement < -.2f)
            {
                rb2d.AddForce(Vector2.right * xMovement * Time.deltaTime * MOVEMENT_SPEED);
                playerAnimator.SetBool("Walking", true);

                playerRenderer.flipX = xMovement < 0; // If we are walking left flip the animation
            }                        
            else
            {
                playerAnimator.SetBool("Walking", false);
            }


            float jumpMovement = Input.GetAxis("Jump");

            if (jumpMovement > .5 && IsOnTheGround)
            {
                rb2d.AddForce(Vector2.up * JUMP_SPEED);
                IsOnTheGround = false;
            }
        }           
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            IsOnTheGround = true;
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            IsOnTheGround = false;
    }
}
