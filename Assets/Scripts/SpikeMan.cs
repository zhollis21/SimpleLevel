using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMan : SingleDirectionEnemy
{

    private Actions currentAction;

    private enum Actions { Standing, Walking, Jumping }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        enemyMovementDirection = MovementDirection.Horizontal;
    }

    // Update is called once per frame
    void Update()
    {

        if ((enemyMovementType == MovementPattern.Chase || enemyMovementType == MovementPattern.PatrolAndChase) && playerTransform != null)
        {
            // If we weren't already walking lets start now
            if (currentAction != Actions.Walking)
            {
                enemyAnimator.SetTrigger("Walk"); 
                currentAction = Actions.Walking;
            }

            MoveTowards(playerTransform.position.x);            
        }
        else if ((enemyMovementType == MovementPattern.Patrol || enemyMovementType == MovementPattern.PatrolAndChase) && patrolPoints.Count > 0)
        {
            // If we weren't already walking lets start now
            if (currentAction != Actions.Walking)
            {
                enemyAnimator.SetTrigger("Walk");
                currentAction = Actions.Walking;
            }

            Patrol();
        }
        else if (currentAction != Actions.Standing)
        {
            enemyAnimator.SetTrigger("Stand");
            currentAction = Actions.Standing;
        }
    }    
}
