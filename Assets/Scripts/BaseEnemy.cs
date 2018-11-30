using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{

    public MovementPattern enemyMovementType;
    public List<Vector2> patrolPoints;
    public float chaseDistance;
    public float movementSpeed;

    public enum MovementPattern { Chase, Patrol, Stay }
    public enum MovementType { Horizontal, Vertical, Both }

    protected CircleCollider2D chaseRangeCollider; //This is used to detect when the player is near 
    protected Transform playerTransform;
    protected Animator enemyAnimator;
    protected SpriteRenderer enemyRenderer;
    protected int patrolPointIndex = 0;

    // Use this for initialization
    protected virtual void Start()
    {
        // If we are supposed to chase the player we create a trigger around the enemy to detect the player in the radius distance
        if (enemyMovementType == MovementPattern.Chase)
        {
            chaseRangeCollider = gameObject.AddComponent<CircleCollider2D>();

            // If for some reason it wasn't able to add it, we can't chase the player
            if (chaseRangeCollider == null)
                enemyMovementType = MovementPattern.Stay;

            chaseRangeCollider.radius = chaseDistance;
            chaseRangeCollider.isTrigger = true;
        }

        // If the enemy is set to patrol, but has no points, it can't patrol
        if (enemyMovementType == MovementPattern.Patrol && patrolPoints.Count < 1)
            enemyMovementType = MovementPattern.Stay;

        enemyAnimator = GetComponent<Animator>();
        enemyRenderer = GetComponent<SpriteRenderer>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player has hit our trigger and we are supposed to be chasing them, we store their position
        if (collision.tag == "Player" && enemyMovementType == MovementPattern.Chase)
        {
            playerTransform = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // If the player has left our chase range, we delete their position
        if (collision.tag == "Player" && enemyMovementType == MovementPattern.Chase)
        {
            playerTransform = null;
        }
    }

    // Moves the player toward the destination based on their movement speed and movement type
    protected void MoveTowards(Vector2 destination, MovementType movementType)
    {
        enemyRenderer.flipX = destination.x < transform.position.x;

        if (movementType == MovementType.Horizontal) // Only changes x value
            transform.position = new Vector2(Mathf.MoveTowards(transform.position.x, destination.x, movementSpeed * Time.deltaTime), transform.position.y);

        else if (movementType == MovementType.Vertical) // Only changes y value
            transform.position = new Vector2(transform.position.x, Mathf.MoveTowards(transform.position.y, destination.y, movementSpeed * Time.deltaTime));

        else // Changes both x and y value
            transform.position = Vector2.MoveTowards(transform.position, destination, movementSpeed * Time.deltaTime);
    }
}
