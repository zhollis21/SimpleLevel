using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMan : BaseEnemy
{
    // Update is called once per frame
    void Update()
    {
        if ((enemyMovementType == MovementPattern.Chase || enemyMovementType == MovementPattern.PatrolAndChase) && playerTransform != null)
        {
            MoveTowards(playerTransform.position, MovementType.Vertical);
        }
        else if ((enemyMovementType == MovementPattern.Patrol || enemyMovementType == MovementPattern.PatrolAndChase) && patrolPoints.Count > 0)
        {
            MoveTowards(patrolPoints[patrolPointIndex], MovementType.Vertical);

            // If we are at the destination, lets set the next destination
            if (Mathf.Abs(patrolPoints[patrolPointIndex].y - transform.position.y) < .1)
            {
                patrolPointIndex++;

                // If we just arrived at the last point, start over
                if (patrolPointIndex == patrolPoints.Count)
                    patrolPointIndex = 0;
            }
        }
    }
}
