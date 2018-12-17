using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualDirectionEnemy : BaseEnemy
{

    public List<Vector2> patrolPoints;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        // If the enemy is set to patrol, but has no points, it can't patrol
        if (enemyMovementType == MovementPattern.Patrol && patrolPoints.Count < 1)
            enemyMovementType = MovementPattern.Stay;

        // If the enemy is set to Patrol and Chase, but has no patrol points, we set it to just chase
        if (enemyMovementType == MovementPattern.PatrolAndChase && patrolPoints.Count < 1)
            enemyMovementType = MovementPattern.Chase;
    }

    // Moves the player toward the destination based on their movement speed
    protected void MoveTowards(Vector2 destination)
    {
        enemyRenderer.flipX = destination.x > transform.position.x;

        transform.position = Vector2.MoveTowards(transform.position, destination, movementSpeed * Time.deltaTime);
    }

    protected void Patrol()
    {
        MoveTowards(patrolPoints[patrolPointIndex]);

        // If we are at the destination, lets set the next destination
        if (Mathf.Abs(patrolPoints[patrolPointIndex].x - transform.position.x) < .1 && Mathf.Abs(patrolPoints[patrolPointIndex].y - transform.position.y) < .1)
        {
            patrolPointIndex++;

            // If we just arrived at the last point, start over
            if (patrolPointIndex == patrolPoints.Count)
                patrolPointIndex = 0;
        }
    }
}