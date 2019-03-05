using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CapsuleCollider2D playerFeetCollider;
    public PolygonCollider2D playerLowerBodyCollider;

    private Rigidbody2D rb2d;
    private RaycastHit2D[] castFeetCollisions;
    private RaycastHit2D[] castLowerBodyCollisions;
    private const int MOVEMENT_SPEED = 800;
    private const int MAX_VELOCITY = 7;
    private const int JUMP_FORCE = 650;
    private const float JUMP_COOLDOWN = .75f;
    private Animator playerAnimator;
    private SpriteRenderer playerRenderer;
    private BoxCollider2D groundTrigger;
    private bool isOnTheGround;
    private bool isAlive = true;
    private Actions currentAction;
    private float timeSinceLastJump;
    private const int ARRAY_LENGTH = 10;

    private enum Actions { Standing, Walking, Jumping }

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerRenderer = GetComponent<SpriteRenderer>();
        currentAction = Actions.Standing;

        Debug.Assert(rb2d != null, "Player Rigidbody2D is null.");
        Debug.Assert(playerAnimator != null, "Player Animator is null.");
        Debug.Assert(playerRenderer != null, "Player SpriteRenderer is null.");
        Debug.Assert(playerFeetCollider != null, "Player Feet Collider 2D is null.");
        Debug.Assert(playerLowerBodyCollider != null, "Player Lower Body Collider 2D is null.");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
            return;

        if (playerFeetCollider != null && playerLowerBodyCollider)
        {
            // Suprisingly this is the simplest way I could find to tell if we are on the ground
            // We cast the foot collider down and then catch up to 10 things it hits
            // If one of them have a tag of ground, then we know we are on or at least very close to the ground
            castFeetCollisions = new RaycastHit2D[ARRAY_LENGTH];
            playerFeetCollider.Cast(Vector2.down, castFeetCollisions, .05f);

            castLowerBodyCollisions = new RaycastHit2D[ARRAY_LENGTH];
            playerLowerBodyCollider.Cast(Vector2.down, castLowerBodyCollisions, .05f);

            for (int i = 0; i < ARRAY_LENGTH; i++)
            {
                if ((castFeetCollisions[i].collider != null && castFeetCollisions[i].collider.tag == "Ground") ||
                    (castLowerBodyCollisions[i].collider != null && castLowerBodyCollisions[i].collider.tag == "Ground"))
                {
                    isOnTheGround = true;
                    break;
                }
                else
                    isOnTheGround = false;
            }
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
                rb2d.velocity = Vector2.up * rb2d.velocity.y; // We still should hold onto vertical velocity
        }
        
        // Handle Vertical Movement
        float jumpMovement = Input.GetAxis("Jump");
        timeSinceLastJump += Time.deltaTime;

        if (jumpMovement > .5 && isOnTheGround && timeSinceLastJump > JUMP_COOLDOWN)
        {
            // Erase any negative vertical velocity, but keep the horizontal velocity
            if (rb2d.velocity.y < 0)
                rb2d.velocity = Vector2.right * rb2d.velocity.x;

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
