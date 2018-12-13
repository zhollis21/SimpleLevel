using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDirectionEnemy : BaseEnemy
{
    public float startingPatrolPoint;
    public float endingPatrolPoint;

    public enum MovementDirection { Horizontal, Vertical }

    protected List<float> patrolPoints;
    protected MovementDirection enemyMovementDirection;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        patrolPoints = new List<float> { startingPatrolPoint, endingPatrolPoint};
        patrolPoints.Sort(); // Important for Patrol and Chase

        // If the enemy is set to patrol, but has no points, it can't patrol
        if (enemyMovementType == MovementPattern.Patrol && patrolPoints.Count < 1)
            enemyMovementType = MovementPattern.Stay;

        // If the enemy is set to Patrol and Chase, but has no patrol points, we set it to just chase
        if (enemyMovementType == MovementPattern.PatrolAndChase && patrolPoints.Count < 1)
            enemyMovementType = MovementPattern.Chase;
    }

    // Moves the player toward the destination based on their movement speed and movement type
    protected void MoveTowards(float destination)
    {
        if (enemyMovementDirection == MovementDirection.Horizontal) // Only changes x value
        {
            enemyRenderer.flipX = destination < transform.position.x;
            transform.position = new Vector2(Mathf.MoveTowards(transform.position.x, destination, movementSpeed * Time.deltaTime), transform.position.y);
        }
        else // Only changes y value
            transform.position = new Vector2(transform.position.x, Mathf.MoveTowards(transform.position.y, destination, movementSpeed * Time.deltaTime));
    }

    // Moves the player toward the destination based on their range, movement speed and movement type
    // NOTE: startRange needs to always be less than or equal to endRange, so make sure they are in the correct order
    protected void MoveTowardsInRange(float destination, float startRange, float endRange)
    {
        enemyRenderer.flipX = destination < transform.position.x;

        // The destination is out of our range to the left/down
        if (destination < startRange)
            destination = startRange;

        // The destination is out of our range to the right/up
        else if (destination > endRange)
            destination = endRange;

        if (enemyMovementDirection == MovementDirection.Horizontal) // Only changes x value        
            transform.position = new Vector2(Mathf.MoveTowards(transform.position.x, destination, movementSpeed * Time.deltaTime), transform.position.y);

        else // Only changes y value
            transform.position = new Vector2(transform.position.x, Mathf.MoveTowards(transform.position.y, destination, movementSpeed * Time.deltaTime));
    }

    protected void Patrol()
    {
        float movementAxis;

        if (enemyMovementDirection == MovementDirection.Horizontal)
            movementAxis = transform.position.x;
        else
            movementAxis = transform.position.y;

        MoveTowards(patrolPoints[patrolPointIndex]);

        // If we are at the destination, lets set the next destination
        if (Mathf.Abs(patrolPoints[patrolPointIndex] - movementAxis) < .1)
        {
            patrolPointIndex++;

            // If we just arrived at the last point, start over
            if (patrolPointIndex == patrolPoints.Count)
                patrolPointIndex = 0;
        }
    }
}
