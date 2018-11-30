using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public Text scoreText;

    private Rigidbody2D rb2d;
    private const int MOVEMENT_SPEED = 500;
    private const int JUMP_SPEED = 500;
    private Animator playerAnimator;
    private SpriteRenderer playerRenderer;
    private bool IsOnTheGround;
    private int score;

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
        if (rb2d != null)
        {
            // Handle Horizontal Movement
            float xMovement = Input.GetAxisRaw("Horizontal");

            if (xMovement > .2f || xMovement < -.2f)
            {
                rb2d.AddForce(Vector2.right * xMovement * Time.deltaTime * MOVEMENT_SPEED);
                playerAnimator.SetBool("Walking", true);

                playerRenderer.flipX = xMovement < 0; // If we are walking left, flip the animation
            }
            else
            {
                playerAnimator.SetBool("Walking", false);
                
                if (IsOnTheGround) // Stop the player from sliding on the ground
                    rb2d.velocity = Vector2.up * rb2d.velocity.y;
            }


            // Handle Vertical Movement
            float jumpMovement = Input.GetAxis("Jump");

            if (jumpMovement > .5 && IsOnTheGround)
            {
                rb2d.AddForce(Vector2.up * JUMP_SPEED);
                IsOnTheGround = false;
            }

            // Save the player if they fall off
            if (transform.position.y < -15)
            {
                transform.position = Vector3.zero;

                // Take away a point for saving them
                if (score > 0)
                {
                    score--;
                    scoreText.text = "Score: " + score;
                }
            }
        }
    }

    public void AddToScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
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
