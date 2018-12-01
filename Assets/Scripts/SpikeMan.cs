using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMan : BaseEnemy
{

    private Actions currentAction;

    private enum Actions { Standing, Walking, Jumping }
    
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

        if (enemyMovementType == MovementPattern.Chase && playerTransform != null)
        {
            if (currentAction != Actions.Walking)
            {
                enemyAnimator.SetTrigger("Walk");
                currentAction = Actions.Walking;
            }

            MoveTowards(playerTransform.position, MovementType.Horizontal);            
        }
        else if (enemyMovementType == MovementPattern.Patrol && patrolPoints.Count > 0)
        {
            if (currentAction != Actions.Walking)
            {
                enemyAnimator.SetTrigger("Walk");
                currentAction = Actions.Walking;
            }

            MoveTowards(patrolPoints[patrolPointIndex], MovementType.Horizontal);

            // If we are at the destination, lets set the next destination
            if (Mathf.Abs(patrolPoints[patrolPointIndex].x - transform.position.x) < .1)
            {
                patrolPointIndex++;

                // If we just arrived at the last point, start over
                if (patrolPointIndex == patrolPoints.Count)
                    patrolPointIndex = 0;
            }
        }
        else if (currentAction != Actions.Standing)
        {
            enemyAnimator.SetTrigger("Stand");
            currentAction = Actions.Standing;
        }
    }    
}
