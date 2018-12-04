using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private const int MOVEMENT_SPEED = 500;
    private const int JUMP_SPEED = 500;
    private Animator playerAnimator;
    private SpriteRenderer playerRenderer;
    private bool isOnTheGround;
    private bool isAlive = true;

    // Use this for initialization
    void Start()
    {

        rb2d = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
            return;

        if (rb2d != null)
        {
            // Handle Horizontal Movement
            float xMovement = Input.GetAxisRaw("Horizontal");

            if (xMovement > .2f || xMovement < -.2f)
            {
                playerAnimator.SetBool("IsWalking", true);
                rb2d.AddForce(Vector2.right * xMovement * Time.deltaTime * MOVEMENT_SPEED);

                playerRenderer.flipX = xMovement < 0; // If we are walking left, flip the animation
            }
            else
            {
                playerAnimator.SetBool("IsWalking", false);

                if (isOnTheGround) // Stop the player from sliding on the ground
                    rb2d.velocity = Vector2.up * rb2d.velocity.y;
            }


            // Handle Vertical Movement
            float jumpMovement = Input.GetAxis("Jump");

            if (jumpMovement > .5 && isOnTheGround)
            {
                rb2d.AddForce(Vector2.up * JUMP_SPEED);
                isOnTheGround = false;
            }
        }
    }

    public void Kill()
    {
        isAlive = false;
        playerAnimator.SetBool("IsDead", true);
    }

    public void Revive()
    {
        isAlive = true;
        GameManager.instance.RevivePlayer();
        playerAnimator.SetBool("IsDead", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isOnTheGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isOnTheGround = false;
    }
}
