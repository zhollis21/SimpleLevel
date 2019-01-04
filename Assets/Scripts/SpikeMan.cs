using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMan : SingleDirectionEnemy
{

    private Actions currentAction;

    private enum Actions { Standing, Walking }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        enemyMovementDirection = MovementDirection.Horizontal;
    }

    // Update is called once per frame
    void Update()
    {

        if (enemyMovementType == MovementPattern.Chase && playerTransform != null)
        {
            // If we weren't already walking lets start now
            if (currentAction != Actions.Walking)
            {
                enemyAnimator.SetBool("IsWalking", true);
                currentAction = Actions.Walking;
            }

            MoveTowards(playerTransform.position.x);
        }
        else if (enemyMovementType == MovementPattern.PatrolAndChase && playerTransform != null)
        {
            // If we are chasing the player, but waiting at the end of our range
            if ((playerTransform.position.x <= patrolPoints[0] && transform.position.x == patrolPoints[0]) 
                || (playerTransform.position.x >= patrolPoints[1] && transform.position.x == patrolPoints[1])
                || playerTransform.position.x == transform.position.x)
            {
                if (currentAction != Actions.Standing)
                {
                    enemyAnimator.SetBool("IsWalking", false);
                    currentAction = Actions.Standing;
                }
            }
            else
            {
                // If we weren't already walking lets start now
                if (currentAction != Actions.Walking)
                {
                    enemyAnimator.SetBool("IsWalking", true);
                    currentAction = Actions.Walking;
                }

                MoveTowardsInRange(playerTransform.position.x, patrolPoints[0], patrolPoints[1]);
            }
        }
        else if ((enemyMovementType == MovementPattern.Patrol || enemyMovementType == MovementPattern.PatrolAndChase) && patrolPoints.Count > 0)
        {
            // If we weren't already walking lets start now
            if (currentAction != Actions.Walking)
            {
                enemyAnimator.SetBool("IsWalking", true);
                currentAction = Actions.Walking;
            }

            Patrol();
        }
        else if (currentAction != Actions.Standing)
        {
            enemyAnimator.SetBool("IsWalking", false);
            currentAction = Actions.Standing;
        }
    }
}
