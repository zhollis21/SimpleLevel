using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{

    public MovementPattern enemyMovementType;
    public float movementSpeed;

    public enum MovementPattern { Chase, Patrol, PatrolAndChase, Stay }

    protected Transform playerTransform;
    protected Animator enemyAnimator;
    protected SpriteRenderer enemyRenderer;
    protected int patrolPointIndex = 0;

    // Use this for initialization
    protected virtual void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player has hit our trigger and we are supposed to be chasing them, we store their position
        if (collision.tag == "Player" && (enemyMovementType == MovementPattern.Chase || enemyMovementType == MovementPattern.PatrolAndChase))
        {
            playerTransform = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // If the player has left our chase range, we delete their position
        if (collision.tag == "Player" && (enemyMovementType == MovementPattern.Chase || enemyMovementType == MovementPattern.PatrolAndChase))
        {
            playerTransform = null;
        }
    }
}
