using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private CapsuleCollider2D playerCollider;
    private RaycastHit2D[] castCollisions;
    private const int MOVEMENT_SPEED = 750;
    private const int MAX_VELOCITY = 5;
    private const int JUMP_FORCE = 500;
    private const float JUMP_COOLDOWN = .75f;
    private Animator playerAnimator;
    private SpriteRenderer playerRenderer;
    private BoxCollider2D groundTrigger;
    private bool isOnTheGround;
    private bool isAlive = true;
    private Actions currentAction;
    private float timeSinceLastJump;

    private enum Actions { Standing, Walking, Jumping }

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        currentAction = Actions.Standing;

        Debug.Assert(rb2d != null, "Player Rigidbody2D is null.");
        Debug.Assert(playerAnimator != null, "Player Animator is null.");
        Debug.Assert(playerRenderer != null, "Player SpriteRenderer is null.");
        Debug.Assert(playerCollider != null, "Player Capsule Collider 2D is null.");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
            return;

        // Suprisingly this is the simplest way I could find to tell if we are on the ground
        castCollisions = new RaycastHit2D[10];
        playerCollider.Cast(Vector2.down, castCollisions, .1f);
        for (int i = 0; i < castCollisions.Length; i++)
        {
            if (castCollisions[i].collider != null && castCollisions[i].collider.tag == "Ground")
            {
                isOnTheGround = true;
                break;
            }
            else
                isOnTheGround = false;
        }
                
        // Handle Horizontal Movement
        float xMovement = Input.GetAxisRaw("Horizontal");

        if (xMovement > .2f)
        {
            if (currentAction != Actions.Walking)
            {
                playerAnimator.SetBool("IsWalking", true);
                currentAction = Actions.Walking;
            }

            if (rb2d.velocity.x < MAX_VELOCITY)
                rb2d.AddForce(Vector2.right * xMovement * Time.deltaTime * MOVEMENT_SPEED);

            if (playerRenderer.flipX)
                playerRenderer.flipX = false;
        }
        else if (xMovement < -.2f)
        {
            if (currentAction != Actions.Walking)
            {
                playerAnimator.SetBool("IsWalking", true);
                currentAction = Actions.Walking;
            }

            if (rb2d.velocity.x > -MAX_VELOCITY)
                rb2d.AddForce(Vector2.right * xMovement * Time.deltaTime * MOVEMENT_SPEED);

            if (!playerRenderer.flipX)
                playerRenderer.flipX = true;
        }
        else
        {
            if (currentAction != Actions.Standing)
            {
                playerAnimator.SetBool("IsWalking", false);
                currentAction = Actions.Standing;
            }

            if (isOnTheGround) // Stop the player from sliding on the ground
                rb2d.velocity = Vector2.up * rb2d.velocity.y;
        }
        
        // Handle Vertical Movement
        float jumpMovement = Input.GetAxis("Jump");
        timeSinceLastJump += Time.deltaTime;

        if (jumpMovement > .5 && isOnTheGround && timeSinceLastJump > JUMP_COOLDOWN)
        {
            rb2d.AddForce(Vector2.up * JUMP_FORCE);
            isOnTheGround = false;
            timeSinceLastJump = 0;
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
}
