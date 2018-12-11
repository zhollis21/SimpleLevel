using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMan : SingleDirectionEnemy
{

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        enemyMovementDirection = MovementDirection.Vertical;
    }

    // Update is called once per frame
    void Update()
    {
        if ((enemyMovementType == MovementPattern.Chase || enemyMovementType == MovementPattern.PatrolAndChase) && playerTransform != null)
        {
            MoveTowards(playerTransform.position.y);
        }
        else if ((enemyMovementType == MovementPattern.Patrol || enemyMovementType == MovementPattern.PatrolAndChase) && patrolPoints.Count > 0)
        {
            Patrol();
        }
    }
}
